using Library.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// ������� ���������� ��� CRUD-�������� ��� ����������
/// </summary>
/// <typeparam name="TDto">DTO ��� Get-��������</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO ��� Post/Put-��������</typeparam>
/// <typeparam name="TKey">��� �������������� DTO</typeparam>
/// <param name="appService">������ ��� ����������� DTO</param>
/// <param name="logger">������</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// ���������� ����� ������
    /// </summary>
    /// <param name="newDto">����� ������</param>
    /// <returns>����������� ������</returns> 
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {@dto} parameter", nameof(Create), GetType().Name, newDto);
        try
        {
            var res = appService.Create(newDto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(this.Create), res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Create), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// ��������� ��������� ������
    /// </summary>
    /// <param name="id">�������������</param>
    /// <param name="newDto">���������� ������</param>
    /// <returns>����������� ������</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Edit(TKey id, TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{@dto} parameters", nameof(Edit), GetType().Name, id, newDto);
        try
        {
            var res = appService.Update(newDto, id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Edit), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Edit), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    /// <param name="id">�������������</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public IActionResult Delete(TKey id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            appService.Delete(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Delete), GetType().Name);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Delete), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
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
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var res = appService.GetAll();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAll), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// ��������� ������ �� ��������������
    /// </summary>
    /// <param name="id">�������������</param>
    /// <returns>������</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Get(TKey id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var res = appService.Get(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Get), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}