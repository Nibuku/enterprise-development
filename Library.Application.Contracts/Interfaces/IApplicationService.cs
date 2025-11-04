namespace Library.Application.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для CRUD операций
/// </summary>
/// <typeparam name="TGetDto">DTO для Get-запросов</typeparam>
/// <typeparam name="TCreateDto">DTO для Post/Put-запросов</typeparam>
/// <typeparam name="TKey">Тип идентификатора DTO</typeparam>
public interface IApplicationService<TGetDto, TCreateDto, TKey>
{
    /// <summary>
    /// Создание DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns></returns>
    public Task<TGetDto> Create(TCreateDto dto);

    /// <summary>
    /// Получение DTO по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор DTO</param>
    /// <returns></returns>
    public Task<TGetDto> Get(TKey dtoId);

    /// <summary>
    /// Получение всего списка DTO
    /// </summary>
    /// <returns></returns>
    public Task<List<TGetDto>> GetAll();

    /// <summary>
    /// Обновление DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <param name="dtoId">Идентификатор DTO</param> 
    /// <returns></returns>
    public Task<TGetDto> Update(TCreateDto dto, TKey dtoId);

    /// <summary>
    /// Удаление DTO
    /// </summary>
    /// <param name="dtoId">Идентификатор DTO</param>
    public Task Delete(TKey dtoId);
}
