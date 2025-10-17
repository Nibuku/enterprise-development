using AutoMapper;
using Library.Application.Dtos.AnaliticsDtos;
using Library.Infrastructure.Repositories;

namespace Library.Application.Services;

public class LibraryAnalyticsService(
    BookCheckoutRepository checkoutRepository,
    BookRepository bookRepository,
    BookReaderRepository readerRepository,
    IMapper mapper) : ILibraryAnalyticsService
{

    public List<BookWithCountDto> GetIssuedBooksSortedByTitle(DateOnly date)
    {
        var checkouts = checkoutRepository.ReadAll();
        var books = bookRepository.ReadAll();

        var allBookCheckouts = checkouts
            .Where(r => r.LoanDate <= date && r.LoanDate.AddDays(r.LoanDays) >= date)
            .GroupBy(r => r.Book.Id)
            .Join(books,
                r => r.Key,
                b => b.Id,
                (r, b) =>
                {
                    var dto = mapper.Map<BookWithCountDto>(b);
                    dto.Count = r.Count();
                    return dto;
                })
            .OrderBy(b => b.Title)
            .ToList();

        return allBookCheckouts;
    }

    // 2. Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период.
    public List<BookReaderWithCountDto> GetTopReadersByPeriod(DateOnly start, DateOnly end)
    {
        var checkouts = checkoutRepository.ReadAll();
        var readers = readerRepository.ReadAll();

        var topReaders = checkouts
            .Where(r => r.LoanDate >= start && r.LoanDate.AddDays(r.LoanDays) <= end)
            .GroupBy(r => r.Reader.Id)
            .Select(x =>
            {
                var reader = readers.First(c => c.Id == x.Key);
                var dto = mapper.Map<BookReaderWithCountDto>(reader);
                dto.Count = x.Count();
                return dto;
            })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.FullName)
            .Take(5)
            .ToList();

        return topReaders;
    }

    // 3. Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО.
    public List<BookReaderWithDaysDto> GetLongestBorrowers()
    {
        var checkouts = checkoutRepository.ReadAll();
        var readers = readerRepository.ReadAll();

        var topReaders = checkouts
            .GroupBy(r => r.Reader.Id)
            .Select(x =>
            {
                var customer = readers.First(c => c.Id == x.Key);
                var dto = mapper.Map<BookReaderWithDaysDto>(customer);       
                dto.TotalDays= x.Max(r => r.LoanDays);
                return dto;
            })
            .ToList();

        var maxDays = topReaders.Max(x => x.TotalDays);

        var top = topReaders
            .Where(x => x.TotalDays == maxDays)
            .OrderBy(c => c.FullName)
            .ToList();

        return top;
    }

    // 4. Вывести топ 5 наиболее популярных издательств за последний год.
    public List<PublisherCountDto> GetTopPublishersByLastYear(DateOnly start)
    {
        var checkouts = checkoutRepository.ReadAll();
        var books = bookRepository.ReadAll();

        var topFivePublishers = checkouts
            .Where(r => r.LoanDate >= start)

            .Select(r => books.First(b => b.Id == r.Book.Id).Publisher)
            .GroupBy(p => p)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new PublisherCountDto
            {
                Name = g.Key.Name,
                Count = g.Count()
            })
            .ToList();

        return topFivePublishers;
    }

    // 5. Вывести топ 5 наименее популярных книг за последний год.
    public List<BookWithCountDto> GetLeastPopularBooksByLastYear(DateOnly start)
    {
        var records = checkoutRepository.ReadAll();
        var books = bookRepository.ReadAll();
        var recordsInPeriod = records
            .Where(r => r.LoanDate >= start)
            .ToList();

        var bookCounts = recordsInPeriod
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
            .Take(5)
            .ToList();

        return result;
    }
}