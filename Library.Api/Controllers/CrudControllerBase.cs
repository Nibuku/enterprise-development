using Library.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// ������� ���������� ��� CRUD-�������� ��� ����������
/// </summary>
/// <typeparam name="TDto">DTO ��� Get-��������</typeparam>
/// <typeparam name="TCreateDto">DTO ��� Post/Put-��������</typeparam>
/// <typeparam name="TKey">��� Id DTO</typeparam>
/// <param name="appService">������ ��� ������ � DTO</param>
/// <param name="logger">������</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateDto, TKey>(IApplicationService<TDto, TCreateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateDto, TKey>> logger) : ControllerBase
{
    /// <summary>
    /// ��������������� ����� ��� ������������.
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
    /// ���������� ����� ������
    /// </summary>
    /// <param name="newDto">����� ������</param>
    /// <returns>����������� ������</returns> 
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Create(TCreateDto newDto)
    {
        return Logging(nameof(Create), () =>
        {
            var result = appService.Create(newDto);
            return CreatedAtAction(nameof(this.Create), result);
        });
    }

    /// <summary>
    /// ��������� ������ �� Id
    /// </summary>
    /// <param name="id">�������������</param>
    /// <param name="newDto">���������� ������</param>
    /// <returns>����������� ������</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Edit(TKey id, TCreateDto newDto)
    {
        return Logging(nameof(Edit), () =>
        { 
            try
            {
                var result = appService.Update(newDto, id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
    }

    /// <summary>
    /// �������� ������ �� Id
    /// </summary>
    /// <param name="id">�������������</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public IActionResult Delete(TKey id)
    {
        return Logging(nameof(Delete), () =>
        {
            try
            {
                appService.Delete(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            { 
                return NotFound();
            }
        });
    }

    /// <summary>
    /// ��������� ������ ���� ������
    /// </summary>
    /// <returns>������ ���� ������</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IList<TDto>> GetAll()
    {
        return Logging(nameof(GetAll), () =>
        {
            var result = appService.GetAll();
            return Ok(result);
        });
    }

    /// <summary>
    /// ��������� ������ �� Id
    /// </summary>
    /// <param name="id">�������������</param>
    /// <returns>������</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Get(TKey id)
    {
        return Logging(nameof(Get), () =>
        {
            try
            {
                var result = appService.Get(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
    }
}