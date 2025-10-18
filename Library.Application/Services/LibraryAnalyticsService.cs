using AutoMapper;
using Library.Application.Dtos.AnaliticsDtos;
using Library.Application.Interfaces;
using Library.Infrastructure.Repositories;

namespace Library.Application.Services;

/// <summary>
/// Сервис для аналитических запросов.
/// </summary>
public class LibraryAnalyticsService(
    BookCheckoutRepository checkoutRepository,
    BookRepository bookRepository,
    BookReaderRepository readerRepository,
    IMapper mapper) : ILibraryAnalyticsService
{
    /// <summary>
    /// Получает информацию о выданных книгах, упорядоченных по названию.
    /// </summary>
    /// <param name="date">Дата, на которую проверяется наличие книг в выдаче</param>
    /// <returns>Список книг, отсортированный по названию.</returns>
    public List<BookWithCountDto> GetBooksOrderedByTitle(DateOnly date)
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

    /// <summary>
    /// Получает топ-5 читателей, прочитавших наибольшее количество книг за период.
    /// </summary>
    /// <param name="start">Начало периода</param>
    /// <param name="end">Конец периода</param>
    /// <returns>Список из 5 читателей, отсортированный по убыванию количества книг</returns>
    public List<BookReaderWithCountDto> GetTopReadersByNumberOfBooks(DateOnly start, DateOnly end)
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

    /// <summary>
    /// Получает топ-5 читателей, бравших книги на наибольший период времени.
    /// </summary>
    /// <returns>Список из 5 читателей, отсортированный по убыванию количества дней/returns>
    public List<BookReaderWithDaysDto> GetTopReadersByTotalLoanDays()
    {
        var checkouts = checkoutRepository.ReadAll();
        var readers = readerRepository.ReadAll();

        var topReaders = checkouts
            .GroupBy(r => r.Reader.Id)
            .Select(x =>
            {
                var reader = readers.First(c => c.Id == x.Key);
                var dto = mapper.Map<BookReaderWithDaysDto>(reader);       
                dto.TotalDays= x.Sum(r => r.LoanDays);
                return dto;
            })
            .OrderByDescending(r => r.TotalDays)
            .ThenBy(r => r.FullName)
            .Take(5)
            .ToList();

        return topReaders;
    }

    /// <summary>
    /// Получает топ-5 наиболее популярных издательств за год.
    /// </summary>
    /// <param name="start">Началпериода</param>
    /// <returns>Список из 5 издательств, отсортированный по убыванию популярности</returns>
    public List<PublisherCountDto> GetTopPopularPublishersLastYear(DateOnly start)
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

    /// <summary>
    /// Получает топ-5 наименее популярных книг за год.
    /// </summary>
    /// <param name="start">Началпериода</param>
    /// <returns>Список из 5 книг, отсортированный по возрастанию популярности</returns>
    public List<BookWithCountDto> GetTopLeastPopularBooksLastYear(DateOnly start)
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