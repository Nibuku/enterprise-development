using Library.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций над сущностями
/// </summary>
/// <typeparam name="TGetDto">DTO для Get-запросов</typeparam>
/// <typeparam name="TCreateDto">DTO для Post/Put-запросов</typeparam>
/// <typeparam name="TKey">Тип Id DTO</typeparam>
/// <param name="appService">Служба для работы с DTO</param>
/// <param name="logger">Логгер</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TGetDto, TCreateDto, TKey>(IApplicationService<TGetDto, TCreateDto, TKey> appService,
    ILogger<CrudControllerBase<TGetDto, TCreateDto, TKey>> logger): LoggerController<CrudControllerBase<TGetDto, TCreateDto, TKey>>(logger)
{
    /// <summary>
    /// Добавление новой записи
    /// </summary>
    /// <param name="newDto">Новые данные</param>
    /// <returns>Добавленные данные</returns> 
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TGetDto>> Create(TCreateDto newDto)
    {
        return await Logging(nameof(Create), async () =>
        {
            var result = await appService.Create(newDto);
            return CreatedAtAction(nameof(this.Create), result);
        });
    }

    /// <summary>
    /// Изменение данных по Id
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="newDto">Измененные данные</param>
    /// <returns>Обновленные данные</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TGetDto>> Edit(TKey id, TCreateDto newDto)
    {
        return await Logging(nameof(Edit), async () =>
        { 
            try
            {
                var result = await appService.Update(newDto, id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
    }

    /// <summary>
    /// Удаление данных по Id
    /// </summary>
    /// <param name="id">Идентификатор</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(TKey id)
    {
        return await Logging(nameof(Delete), async () =>
        {
            try
            {
                await appService.Delete(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            { 
                return NotFound();
            }
        });
    }

    /// <summary>
    /// Получение списка всех данных
    /// </summary>
    /// <returns>Список всех данных</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TGetDto>>> GetAll()
    {
        return await Logging(nameof(GetAll), async () =>
        {
            var result = await appService.GetAll();
            return Ok(result);
        });
    }

    /// <summary>
    /// Получение данных по Id
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Данные</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TGetDto>> Get(TKey id)
    {
        return await Logging(nameof(Get), async () =>
        {
            try
            {
                var result = await appService.Get(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        });
    }
}