using AutoMapper;
using Library.Application.Dtos.AnaliticsDtos;
using Library.Domain.Models;
using Library.Infrastructure.Repositories;

namespace Library.Application.Services;

public class LibraryAnalyticsService(
    BookCheckoutRepository checkoutRepository,
    BookRepository bookRepository,
    BookReaderRepository readerRepository,
    PublisherRepository publisherRepository,
    IMapper mapper) : ILibraryAnalyticsService
{
    // --- Вспомогательные методы (для получения всех данных за один раз) ---

    // Внимание: Этот метод загружает ВСЕ данные, что неэффективно для больших БД.
    private (List<BookCheckout> checkouts, List<Book> books, List<BookReader> readers, List<Publisher> publishers) GetAllEntities()
    {
        // Загружаем все необходимые сущности в память
        var checkouts = checkoutRepository.ReadAll();
        var books = bookRepository.ReadAll();
        var readers = readerRepository.ReadAll();
        var publishers = publisherRepository.ReadAll();
        return (checkouts, books, readers, publishers);
    }

    // 1. Вывести информацию о выданных книгах, упорядоченных по названию.
    public List<BookWithCountDto> GetIssuedBooksSortedByTitle()
    {
        var (checkouts, books, _, _) = GetAllEntities();
        var today = DateOnly.FromDateTime(DateTime.Today);

        var activeCheckouts = checkouts
            .Where(c => c.LoanDate.AddDays(c.LoanDays) >= today)
            .ToList();

        var issuedBooksDto = activeCheckouts
            .GroupBy(c => c.Book.Id)
            .Select(group =>
            {
                var book = books.First(b => b.Id == group.Key);

                var dto = mapper.Map<BookWithCountDto>(book);
                dto.Count = group.Count(); 
                return dto;
            })
            .OrderBy(b => b.Title)
            .ToList();

        return issuedBooksDto;
    }

    // 2. Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период.
    public List<BookReaderWithCountDto> GetTopReadersByPeriod(DateTime startDate, DateTime endDate, int count = 5)
    {
        var (checkouts, _, readers, _) = GetAllEntities();
        var startDateOnly = DateOnly.FromDateTime(startDate);
        var endDateOnly = DateOnly.FromDateTime(endDate);

        var recordsInPeriod = checkouts
            .Where(r => r.LoanDate >= startDateOnly && r.LoanDate <= endDateOnly)
            .ToList();

        var topReadersData = recordsInPeriod
            .GroupBy(r => r.Reader.Id)
            .OrderByDescending(g => g.Count())
            .Take(count)
            .ToList();

        var result = topReadersData.Select(group =>
        {
            var reader = readers.First(r => r.Id == group.Key);
            var dto = mapper.Map<BookReaderWithCountDto>(reader);
            dto.Count = group.Count();
            return dto;
        })
        .ToList();

        return result;
    }

    // 3. Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО.
    public List<BookReaderWithDayDto> GetLongestBorrowers()
    {
        var (checkouts, _, readers, _) = GetAllEntities();

        var readersWithMaxDuration = checkouts
            .GroupBy(c => c.Reader.Id)
            .Select(group => new
            {
                ReaderId = group.Key,
                MaxDuration = group.Max(c => c.LoanDays)
            })
            .ToList();

        if (readersWithMaxDuration.Count == 0) return [];

        var overallMaxDuration = readersWithMaxDuration.Max(x => x.MaxDuration);

        var longestBorrowersIds = readersWithMaxDuration
            .Where(x => x.MaxDuration == overallMaxDuration)
            .Select(x => x.ReaderId)
            .ToList();

        var result = readers
            .Where(r => longestBorrowersIds.Contains(r.Id))
            .Select(reader =>
            {
                var dto = mapper.Map<BookReaderWithDayDto>(reader);
                dto.TotalDays = overallMaxDuration;
                return dto;
            })
            .OrderBy(r => r.FullName)
            .ToList();

        return result;
    }

    // 4. Вывести топ 5 наиболее популярных издательств за последний год.
    public List<PublisherCountDto> GetTopPublishersByLastYear(int count)
    {
        var (checkouts, books, _, publishers) = GetAllEntities();
        var oneYearAgo = DateOnly.FromDateTime(DateTime.Today.AddYears(-1));

        var recordsLastYear = checkouts
            .Where(c => c.LoanDate >= oneYearAgo)
            .ToList();

        var topPublishersData = recordsLastYear
            .Join(books,
                  record => record.Book.Id,
                  book => book.Id,
                  (record, book) => book) 
            .GroupBy(book => book.Publisher.Id)
            .Select(group => new
            {
                PublisherId = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(count)
            .ToList();

        var result = topPublishersData.Select(x =>
        {
            var publisher = publishers.First(p => p.Id == x.PublisherId);
            var dto = mapper.Map<PublisherCountDto>(publisher);
            dto.Count = x.Count;
            return dto;
        })
        .ToList();

        return result;
    }

    // 5. Вывести топ 5 наименее популярных книг за последний год.
    public List<BookWithCountDto> GetLeastPopularBooksByLastYear(int count)
    {
        var (checkouts, books, _, _) = GetAllEntities();
        var oneYearAgo = DateOnly.FromDateTime(DateTime.Today.AddYears(-1));

        var recordsLastYear = checkouts
            .Where(r => r.LoanDate >= oneYearAgo)
            .ToList();

        var bookCounts = recordsLastYear
            .GroupBy(r => r.Book.Id)
            .ToDictionary(g => g.Key, g => g.Count());

        var result = books
            .Select(book =>
            {
                var dto = mapper.Map<BookWithCountDto>(book);
                dto.Count = bookCounts.GetValueOrDefault(book.Id, 0);
                return dto;
            })
            .OrderBy(dto => dto.Count) 
            .ThenBy(dto => dto.Title)
            .Take(count)
            .ToList();

        return result;
    }
}