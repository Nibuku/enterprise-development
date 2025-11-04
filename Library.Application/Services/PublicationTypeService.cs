using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис, обеспечивающий CRUD-операции для работы с типами публикаций.
/// </summary>
public class PublicationTypeService(
    IRepositoryAsync<PublicationType, int> typeRepository,
    IMapper mapper) : IApplicationService<PublicationTypeGetDto, PublicationTypeCreateDto, int>
{
    /// <summary>
    /// Вносит запись о новом типе публикаций.
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>DTO созданного типа</returns>
    public async Task<PublicationTypeGetDto> Create(PublicationTypeCreateDto dto)
    {
        var type = mapper.Map<PublicationType>(dto);
        await typeRepository.Create(type);

        return mapper.Map<PublicationTypeGetDto>(type);
    }

    /// <summary>
    /// Получение информации о типе публикации по Id.
    /// </summary>
    /// <param name="dtoId"> Id типа </param>
    /// <returns>DTO типа публикации</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если тип публикации с указанным Id не найден.</exception>
    public async Task<PublicationTypeGetDto> Get(int dtoId)
    {
        var type = await typeRepository.Read(dtoId)
            ?? throw new InvalidOperationException($"Тип публикации с ID {dtoId} не найден.");

        return mapper.Map<PublicationTypeGetDto>(type);
    }

    /// <summary>
    /// Получает список с DTO всех типов публикации.
    /// </summary>
    /// <returns>Список DTO всех типов</returns>
    public async Task<List<PublicationTypeGetDto>> GetAll()
    {
        var types =await typeRepository.ReadAll();
        return mapper.Map<List<PublicationTypeGetDto>>(types);
    }

    /// <summary>
    /// Обновляет информацию о типе публикации по Id.
    /// </summary>
    /// <param name="dto">DTO с данными для обновления</param>
    /// <param name="dtoId">Id обновляемого типа</param>
    /// <returns>DTO типа публикации</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если тип издания с указанным Id не найден.</exception>
    public async Task<PublicationTypeGetDto> Update(PublicationTypeCreateDto dto, int dtoId)
    {
        var typeToUpdate =await typeRepository.Read(dtoId) ?? throw new InvalidOperationException($"Тип публикации с ID {dtoId} не найден.");
        mapper.Map(dto, typeToUpdate);
        await typeRepository.Update(typeToUpdate);
        return mapper.Map<PublicationTypeGetDto>(typeToUpdate);
    }

    /// <summary>
    /// Удаляет тип публикации по Id.
    /// </summary>
    /// <param name="dtoId">Id удаляемого типа/param>
    /// <exception cref="InvalidOperationException">Вызывается, если тип публикации с указанным Id не найдена.</exception>
    public async Task Delete(int dtoId)
    {        
        if (!await typeRepository.Delete(dtoId))
            throw new InvalidOperationException($"Тип публикации с ID {dtoId} не найдена.");
    }
}
