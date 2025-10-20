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
    IRepository<BookReader, int> bookReaderRepository,
    IMapper mapper) : IApplicationService<BookReaderGetDto, BookReaderCreateDto, int>
{
    /// <summary>
    /// Вносит нового читателя.
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>DTO созданной записи</returns>
    public BookReaderGetDto Create(BookReaderCreateDto dto)
    {
        var bookReader = mapper.Map<BookReader>(dto);
        bookReaderRepository.Create(bookReader);

        return mapper.Map<BookReaderGetDto>(bookReader);
    }

    /// <summary>
    /// Получение информации о читателе по Id.
    /// </summary>
    /// <param name="dtoId"> Id читателя</param>
    /// <returns>DTO читателя</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если читатель с указанным Id не найден.</exception>
    public BookReaderGetDto Get(int dtoId)
    {
        var bookReader = bookReaderRepository.Read(dtoId)
            ?? throw new InvalidOperationException($"Читатель с ID {dtoId} не найден.");

        return mapper.Map<BookReaderGetDto>(bookReader);
    }

    /// <summary>
    /// Получает список с DTO всех читателей.
    /// </summary>
    /// <returns>Список DTO всех записей</returns>
    public List<BookReaderGetDto> GetAll()
    {
        var bookReaders = bookReaderRepository.ReadAll();
        return mapper.Map<List<BookReaderGetDto>>(bookReaders);
    }

    /// <summary>
    /// Обновляет информацию о читателе по Id.
    /// </summary>
    /// <param name="dto">DTO с данными для обновления</param>
    /// <param name="dtoId">Id обновляемой записи</param>
    /// <returns>DTO обновленной записи читателя</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если читатель с указанным Id не найден.</exception>
    public BookReaderGetDto Update(BookReaderCreateDto dto, int dtoId)
    {
        var readerToUpdate = bookReaderRepository.Read(dtoId) ?? throw new InvalidOperationException($"Читатель с ID {dtoId} не найден.");
        mapper.Map(dto, readerToUpdate);
        bookReaderRepository.Update(readerToUpdate);
        return mapper.Map<BookReaderGetDto>(readerToUpdate);
    }

    /// <summary>
    /// Удаляет запись о читателе по Id.
    /// </summary>
    /// <param name="dtoId">Id удаляемого читателя/param>
    /// <exception cref="InvalidOperationException">Вызывается, если читатель с указанным Id не найден.</exception>
    public void Delete(int dtoId)
    {
        if (!bookReaderRepository.Delete(dtoId))
            throw new InvalidOperationException($"Читатель с ID {dtoId} не найдена.");
    }
}
