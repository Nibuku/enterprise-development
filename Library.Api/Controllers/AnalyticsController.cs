using Library.Application.Dtos.AnaliticsDtos;
using Library.Application.Services;
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
    LibraryAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Вспомогательный метод для логированияк.
    /// </summary>
    protected ActionResult Logging(string method, Func<ActionResult> action)
    {
        logger.LogInformation("START: {Method}", method);
        try
        {
            var result = action();
            var count = 0;
            if (result is OkObjectResult okResult && okResult.Value != null)
            {
                if (okResult.Value is System.Collections.IEnumerable collection)
                {
                    count = collection.Cast<object>().Count();
                }
                else count = 1;
            }
            logger.LogInformation("SUCCESS: {Method}. Found {Count} records.", method, count);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR: {Method} failed.", method);
            return StatusCode(500, $"Server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Возвращает все книги, которые находятся на руках у читателей на текущую дату.
    /// </summary>
    [HttpGet("issued-books")]
    [ProducesResponseType(typeof(List<BookWithCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<BookWithCountDto>> GetBooksOrderedByTitle()
    {
        return Logging(nameof(GetBooksOrderedByTitle), () =>
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var result = analyticsService.GetBooksOrderedByTitle(today);
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
    public ActionResult<List<BookReaderWithCountDto>> GetTopReadersByNumberOfBooks(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end)
    {
        return Logging(nameof(GetTopReadersByNumberOfBooks), () =>
        {
            var result = analyticsService.GetTopReadersByNumberOfBooks(start, end);
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
    public ActionResult<List<BookReaderWithDaysDto>> GetTopReadersByTotalLoanDays()
    {
        return Logging(nameof(GetTopReadersByTotalLoanDays), () =>
        {
            var result = analyticsService.GetTopReadersByTotalLoanDays();
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
    public ActionResult<List<PublisherCountDto>> GetTopPopularPublishersLastYear(
        [FromQuery] DateOnly? start = null)
    {
        return Logging(nameof(GetTopPopularPublishersLastYear), () =>
        {
            var startDate = start ?? DateOnly.FromDateTime(DateTime.Today.AddYears(-1));
            var result = analyticsService.GetTopPopularPublishersLastYear(startDate);
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
    public ActionResult<List<BookWithCountDto>> GetTopLeastPopularBooksLastYear(
        [FromQuery] DateOnly? start = null)
    {
        return Logging(nameof(GetTopLeastPopularBooksLastYear), () =>
        {
            var startDate = start ?? DateOnly.FromDateTime(DateTime.Today.AddYears(-1));
            var result = analyticsService.GetTopLeastPopularBooksLastYear(startDate);
            return Ok(result);
        });
    }
}