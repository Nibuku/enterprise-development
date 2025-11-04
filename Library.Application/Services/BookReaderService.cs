using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис, обеспечивающий CRUD-операции для работы с читателями.
/// </summary>
public class BookReaderService(
    IRepositoryAsync<BookReader, int> bookReaderRepository,
    IMapper mapper) : IApplicationService<BookReaderGetDto, BookReaderCreateDto, int>
{
    /// <summary>
    /// Вносит нового читателя.
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>DTO созданной записи</returns>
    public async Task<BookReaderGetDto> Create(BookReaderCreateDto dto)
    {
        var bookReader = mapper.Map<BookReader>(dto);
        await bookReaderRepository.Create(bookReader);

        return mapper.Map<BookReaderGetDto>(bookReader);
    }

    /// <summary>
    /// Получение информации о читателе по Id.
    /// </summary>
    /// <param name="dtoId"> Id читателя</param>
    /// <returns>DTO читателя</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если читатель с указанным Id не найден.</exception>
    public async Task<BookReaderGetDto> Get(int dtoId)
    {
        var bookReader = await bookReaderRepository.Read(dtoId)
            ?? throw new InvalidOperationException($"Читатель с ID {dtoId} не найден.");

        return mapper.Map<BookReaderGetDto>(bookReader);
    }

    /// <summary>
    /// Получает список с DTO всех читателей.
    /// </summary>
    /// <returns>Список DTO всех записей</returns>
    public async Task<List<BookReaderGetDto>> GetAll()
    {
        var bookReaders =await bookReaderRepository.ReadAll();
        return mapper.Map<List<BookReaderGetDto>>(bookReaders);
    }

    /// <summary>
    /// Обновляет информацию о читателе по Id.
    /// </summary>
    /// <param name="dto">DTO с данными для обновления</param>
    /// <param name="dtoId">Id обновляемой записи</param>
    /// <returns>DTO обновленной записи читателя</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если читатель с указанным Id не найден.</exception>
    public async Task<BookReaderGetDto> Update(BookReaderCreateDto dto, int dtoId)
    {
        var readerToUpdate = await bookReaderRepository.Read(dtoId) ?? throw new InvalidOperationException($"Читатель с ID {dtoId} не найден.");
        mapper.Map(dto, readerToUpdate);
        await bookReaderRepository.Update(readerToUpdate);
        return mapper.Map<BookReaderGetDto>(readerToUpdate);
    }

    /// <summary>
    /// Удаляет запись о читателе по Id.
    /// </summary>
    /// <param name="dtoId">Id удаляемого читателя/param>
    /// <exception cref="InvalidOperationException">Вызывается, если читатель с указанным Id не найден.</exception>
    public async Task Delete(int dtoId)
    {
        if (!await bookReaderRepository.Delete(dtoId))
            throw new InvalidOperationException($"Читатель с ID {dtoId} не найдена.");
    }
}
