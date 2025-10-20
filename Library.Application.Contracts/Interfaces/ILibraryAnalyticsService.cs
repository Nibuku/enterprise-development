using Library.Application.Contracts.Dtos.AnaliticsDtos;

namespace Library.Application.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для аналитикческих запросов.
/// </summary>
public interface ILibraryAnalyticsService
{
    /// <summary>
    /// Выводит информацию о выданных книгах, упорядоченных по названию.
    /// </summary>
    /// <param name="date">Дата, на которую проверяется выданные книги</param>
    /// <returns>Список книг, отсортированный по названию</returns>
    public List<BookWithCountDto> GetBooksOrderedByTitle(DateOnly date);

    /// <summary>
    /// Выводит информацию о топ 5 читателей, прочитавших больше всего книг за заданный период.
    /// </summary>
    /// <param name="start">Начало периода </param>
    /// <param name="end">Конец периода</param>
    /// <returns>Список читателей, отсортированный по убыванию количества</returns>
    public List<BookReaderWithCountDto> GetTopReadersByNumberOfBooks(DateOnly start, DateOnly end);

    /// <summary>
    /// Выводит информацию о читателях, бравших книги на наибольший период времени.
    /// </summary>
    /// <returns>Список читателей с периодом выдачи, отсортированный по ФИО</returns>
    public List<BookReaderWithDaysDto> GetTopReadersByTotalLoanDays();

    /// <summary>
    /// Вывести топ-5 наиболее популярных издательств за последний год.
    /// </summary>
    /// <param name="start">Начальная дата периода (год назад от даты конца периода)</param>
    /// <returns>Список издательств, отсортированный по убыванию популярности</returns>
    public List<PublisherCountDto> GetTopPopularPublishersLastYear(DateOnly start);

    /// <summary>
    /// Вывести топ-5 наименее популярных книг за последний год.
    /// </summary>
    /// <param name="start">Начальная дата периода (год назад от даты конца периода)</param>
    /// <returns>Список книг, отсортированный по возрастанию популярности</returns>
    public List<BookWithCountDto> GetTopLeastPopularBooksLastYear(DateOnly start);
}

