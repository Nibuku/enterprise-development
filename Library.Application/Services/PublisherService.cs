using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис, обеспечивающий CRUD-операции для работы с издательствами.
/// </summary>
public class PublisherService(
    IRepositoryAsync<Publisher, int> publisherRepository,
    IMapper mapper) : IApplicationService<PublisherGetDto, PublisherCreateDto, int>
{
    /// <summary>
    /// Вносит запись о новом издательстве.
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>DTO созданной записи</returns>
    public async Task<PublisherGetDto> Create(PublisherCreateDto dto)
    {
        var publisher = mapper.Map<Publisher>(dto);
        await publisherRepository.Create(publisher);

        return mapper.Map<PublisherGetDto>(publisher);
    }

    /// <summary>
    /// Получение информации об издательстве по Id.
    /// </summary>
    /// <param name="dtoId"> Id издательства </param>
    /// <returns>DTO читателя</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если издательство с указанным Id не найдено.</exception>
    public async Task<PublisherGetDto> Get(int dtoId)
    {
        var publisher =await publisherRepository.Read(dtoId)
            ?? throw new InvalidOperationException($"Издательство с ID {dtoId} не найден.");

        return mapper.Map<PublisherGetDto>(publisher);
    }

    /// <summary>
    /// Получает список с DTO всех издательств.
    /// </summary>
    /// <returns>Список DTO всех записей</returns>
    public async Task<List<PublisherGetDto>> GetAll()
    {
        var publishers =await publisherRepository.ReadAll();
        return mapper.Map<List<PublisherGetDto>>(publishers);
    }

    /// <summary>
    /// Обновляет информацию об издательстве по Id.
    /// </summary>
    /// <param name="dto">DTO с данными для обновления</param>
    /// <param name="dtoId">Id обновляемого издательства</param>
    /// <returns>DTO издательства</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если издательство с указанным Id не найдено.</exception>
    public async Task<PublisherGetDto> Update(PublisherCreateDto dto, int dtoId)
    {
        var publisherToUpdate =await publisherRepository.Read(dtoId) ?? throw new InvalidOperationException($"Издательство с ID {dtoId} не найден.");
        mapper.Map(dto, publisherToUpdate);
        await publisherRepository.Update(publisherToUpdate);
        return mapper.Map<PublisherGetDto>(publisherToUpdate);
    }

    /// <summary>
    /// Удаляет запись об издательстве по Id.
    /// </summary>
    /// <param name="dtoId">Id удаляемого издательства/param>
    /// <exception cref="InvalidOperationException">Вызывается, если издательство с указанным Id не найдено.</exception>
    public async Task Delete(int dtoId)
    {
        if (!await publisherRepository.Delete(dtoId))
            throw new InvalidOperationException($"Издательство с ID {dtoId} не найдена.");
    }
}
