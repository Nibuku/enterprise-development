using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// Контроллер для логирования
/// </summary>
/// <typeparam name="TController">Тип контроллера</typeparam>
[Route("api/logger")]
[ApiController]
public abstract class LoggerController<TController>(ILogger<TController> logger): ControllerBase
{
    private readonly ILogger<TController> _logger = logger;

    /// <summary>
    /// Метод для логирования.
    /// </summary>
    protected async Task<ActionResult> Logging(string method, Func<Task<ActionResult>> action)
    {
        _logger.LogInformation("START: {Method}", method);
        try
        {
            var result = await action();
            var count = 0;
            if (result is OkObjectResult okResult && okResult.Value != null)
            {
                if (okResult.Value is System.Collections.IEnumerable collection)
                {
                    count = collection.Cast<object>().Count();
                }
                else count = 1;
            }
            _logger.LogInformation("SUCCESS: {Method}. Found {Count} records.", method, count);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR: {Method} failed.", method);
            return StatusCode(500, $"Server error: {ex.Message}");
        }
    }
}
