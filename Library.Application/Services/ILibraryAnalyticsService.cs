using Library.Application.Dtos;
using Library.Application.Dtos.AnaliticsDtos;

namespace Library.Application.Services;
public interface ILibraryAnalyticsService
{
    // 1. Вывести информацию о выданных книгах, упорядоченных по названию.
    public List<BookWithCountDto> GetIssuedBooksSortedByTitle(DateOnly date);

    // 2. Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период.
    public List<BookReaderWithCountDto> GetTopReadersByPeriod(DateOnly start, DateOnly end);

    // 3. Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО.
    public List<BookReaderWithDaysDto> GetLongestBorrowers();

    // 4. Вывести топ 5 наиболее популярных издательств за последний год.
    public List<PublisherCountDto> GetTopPublishersByLastYear(DateOnly start);

    // 5. Вывести топ 5 наименее популярных книг за последний год.
    public List<BookWithCountDto> GetLeastPopularBooksByLastYear(DateOnly start);
}

