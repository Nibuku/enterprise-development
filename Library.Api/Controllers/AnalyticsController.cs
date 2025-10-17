using Library.Application.Dtos.AnaliticsDtos;
using Library.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет синхронные API-методы для получения библиотечной аналитики.
/// </summary>
/// <param name="analyticsService">Синхронный аналитический сервис.</param>
/// <param name="logger">Логгер для записи информации и ошибок.</param>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(
    ILibraryAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает все книги, которые находятся на руках у читателей на текущую дату.
    /// </summary>
    [HttpGet("issued-books")]
    [ProducesResponseType(typeof(List<BookWithCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<BookWithCountDto>> GetIssuedBooksSortedByTitle()
    {
        const string methodName = nameof(GetIssuedBooksSortedByTitle);
        logger.LogInformation("START: {Method}", methodName);

        try
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var result = analyticsService.GetIssuedBooksSortedByTitle(today);

            logger.LogInformation("SUCCESS: {Method}. Found {Count} records.", methodName, result.Count);
            return result.Count > 0 ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR: {Method} failed.", methodName);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Возвращает топ 5 читателей, прочитавших наибольшее количество книг за заданный период.
    /// Период должен быть полностью покрыт займом (начало займа >= start, конец займа <= end).
    /// </summary>
    /// <param name="start">Дата начала периода (включительно).</param>
    /// <param name="end">Дата окончания периода (включительно).</param>
    [HttpGet("top-readers-by-count")]
    [ProducesResponseType(typeof(List<BookReaderWithCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<BookReaderWithCountDto>> GetTopReadersByPeriod(
        [FromQuery] DateOnly start,
        [FromQuery] DateOnly end)
    {
        const string methodName = nameof(GetTopReadersByPeriod);
        logger.LogInformation("START: {Method} with period {Start} to {End}", methodName, start, end);

        try
        {
            var result = analyticsService.GetTopReadersByPeriod(start, end);

            logger.LogInformation("SUCCESS: {Method}. Found {Count} records.", methodName, result.Count);
            return result.Count > 0 ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR: {Method} failed.", methodName);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Возвращает читателей с самой большой максимальной длительностью одного займа (LoanDays).
    /// </summary>
    [HttpGet("longest-borrowers")]
    [ProducesResponseType(typeof(List<BookReaderWithDaysDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<BookReaderWithDaysDto>> GetLongestBorrowers()
    {
        const string methodName = nameof(GetLongestBorrowers);
        logger.LogInformation("START: {Method}", methodName);

        try
        {
            var result = analyticsService.GetLongestBorrowers();

            logger.LogInformation("SUCCESS: {Method}. Found {Count} records.", methodName, result.Count);
            return result.Count > 0 ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR: {Method} failed.", methodName);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Возвращает топ 5 наиболее популярных издательств, начиная с указанной даты (LoanDate >= start).
    /// </summary>
    /// <param name="start">Дата начала периода (по умолчанию - год назад).</param>
    [HttpGet("top-publishers")]
    [ProducesResponseType(typeof(List<PublisherCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<PublisherCountDto>> GetTopPublishersByLastYear(
        [FromQuery] DateOnly? start = null)
    {
        const string methodName = nameof(GetTopPublishersByLastYear);

        // Если дата не указана, используем "год назад"
        var startDate = start ?? DateOnly.FromDateTime(DateTime.Today.AddYears(-1));

        logger.LogInformation("START: {Method} with start date {Start}", methodName, startDate);

        try
        {
            var result = analyticsService.GetTopPublishersByLastYear(startDate);

            logger.LogInformation("SUCCESS: {Method}. Found {Count} records.", methodName, result.Count);
            return result.Count > 0 ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR: {Method} failed.", methodName);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Возвращает топ 5 наименее популярных книг, начиная с указанной даты (LoanDate >= start).
    /// </summary>
    /// <param name="start">Дата начала периода (по умолчанию - год назад).</param>
    [HttpGet("least-popular-books")]
    [ProducesResponseType(typeof(List<BookWithCountDto>), 200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<BookWithCountDto>> GetLeastPopularBooksByLastYear(
        [FromQuery] DateOnly? start = null)
    {
        const string methodName = nameof(GetLeastPopularBooksByLastYear);

        var startDate = start ?? DateOnly.FromDateTime(DateTime.Today.AddYears(-1));

        logger.LogInformation("START: {Method} with start date {Start}", methodName, startDate);

        try
        {
            var result = analyticsService.GetLeastPopularBooksByLastYear(startDate);

            logger.LogInformation("SUCCESS: {Method}. Found {Count} records.", methodName, result.Count);
            return result.Count > 0 ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR: {Method} failed.", methodName);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}