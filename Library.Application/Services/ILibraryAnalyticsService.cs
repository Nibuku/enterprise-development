using Library.Application.Dtos;
using Library.Application.Dtos.AnaliticsDtos;

namespace Library.Application.Services;
public interface ILibraryAnalyticsService
{
    // 1. Вывести информацию о выданных книгах, упорядоченных по названию.
    public List<BookWithCountDto> GetIssuedBooksSortedByTitle();

    // 2. Вывести информацию о топ 5 читателей, прочитавших больше всего книг за заданный период.
    public List<BookReaderWithCountDto> GetTopReadersByPeriod(DateTime startDate, DateTime endDate, int count = 5);

    // 3. Вывести информацию о читателях, бравших книги на наибольший период времени, упорядочить по ФИО.
    public List<BookReaderWithDayDto> GetLongestBorrowers();

    // 4. Вывести топ 5 наиболее популярных издательств за последний год.
    public List<PublisherCountDto> GetTopPublishersByLastYear(int count);

    // 5. Вывести топ 5 наименее популярных книг за последний год.
    public List<BookWithCountDto> GetLeastPopularBooksByLastYear(int count);
}

