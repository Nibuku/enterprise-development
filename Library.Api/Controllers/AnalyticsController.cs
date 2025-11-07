using Library.Application.Contracts.Dtos.AnaliticsDtos;
using Library.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для аналитических запросов.
/// </summary>
/// <param name="analyticsService">Аналитический сервис.</param>
/// <param name="logger">Логгер для записи информации.</param>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(
    ILibraryAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger): LoggerController<AnalyticsController>(logger)
{
    /// <summary>
    /// Возвращает все книги, которые находятся на руках у читателей на текущую дату.
    /// </summary>
    [HttpGet("issued-books")]
    [ProducesResponseType(typeof(List<BookWithCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<BookWithCountDto>>> GetBooksOrderedByTitle()
    {
        return await Logging(nameof(GetBooksOrderedByTitle), async () =>
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var result =await analyticsService.GetBooksOrderedByTitle(today);
            return Ok(result);
        });
    }

    /// <summary>
    /// Возвращает топ 5 читателей, прочитавших наибольшее количество книг за заданный период.
    /// </summary>
    /// <param name="start">Начало периода.</param>
    /// <param name="end">Конец периода.</param>
    [HttpGet("top-readers-by-count")]
    [ProducesResponseType(typeof(List<BookReaderWithCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<BookReaderWithCountDto>>> GetTopReadersByNumberOfBooks(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end)
    {
        return await Logging(nameof(GetTopReadersByNumberOfBooks), async() =>
        {
            var result = await analyticsService.GetTopReadersByNumberOfBooks(start, end);
            return Ok(result);
        });
    }

    /// <summary>
    /// Возвращает читателей, бравших книги на наибольшее количество дней).
    /// </summary>
    [HttpGet("top-readers-by-days")]
    [ProducesResponseType(typeof(List<BookReaderWithDaysDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<BookReaderWithDaysDto>>> GetTopReadersByTotalLoanDays()
    {
        return await Logging(nameof(GetTopReadersByTotalLoanDays), async () =>
        {
            var result = await analyticsService.GetTopReadersByTotalLoanDays();
            return result.Count > 0 ? Ok(result) : NoContent();
        });
    }

    /// <summary>
    /// Возвращает топ 5 наиболее популярных издательств за год.
    /// </summary>
    /// <param name="start">Дата начала периода</param>
    [HttpGet("top-publishers")]
    [ProducesResponseType(typeof(List<PublisherCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PublisherCountDto>>> GetTopPopularPublishersLastYear(
        [FromQuery] DateOnly? start = null)
    {
        return await Logging(nameof(GetTopPopularPublishersLastYear), async() =>
        {
            var startDate = start ?? DateOnly.FromDateTime(DateTime.Today.AddYears(-1));
            var result =await  analyticsService.GetTopPopularPublishersLastYear(startDate);
            return Ok(result);
        });
    }

    /// <summary>
    /// Возвращает топ 5 наименее популярных книг.
    /// </summary>
    /// <param name="start">Дата начала периода.</param>
    [HttpGet("least-popular-books")]
    [ProducesResponseType(typeof(List<BookWithCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<BookWithCountDto>>> GetTopLeastPopularBooksLastYear(
        [FromQuery] DateOnly? start = null)
    {
        return await Logging(nameof(GetTopLeastPopularBooksLastYear), async () =>
        {
            var startDate = start ?? DateOnly.FromDateTime(DateTime.Today.AddYears(-1));
            var result = await analyticsService.GetTopLeastPopularBooksLastYear(startDate);
            return Ok(result);
        });
    }
}