using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис, обеспечивающий CRUD-операции для работы с книгами.
/// </summary>
public class BookService(
   IRepositoryAsync<Book, int> bookRepository,
   IRepositoryAsync<Publisher, int> publisherRepository,
   IRepositoryAsync<PublicationType, int> publicationTypeRepository,
    IMapper mapper) : IApplicationService<BookGetDto, BookCreateDto, int>
{
    /// <summary>
    /// Вносит запись о новой книге.
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>DTO созданной книги</returns>
    public async Task<BookGetDto> Create(BookCreateDto dto)
    {
        var publisher = await publisherRepository.Read(dto.PublisherId) ?? throw new InvalidOperationException($"Издательство с ID {dto.PublisherId} не найдено.");
        var type = await publicationTypeRepository.Read(dto.PublicationTypeId) ?? throw new InvalidOperationException($"Тип издания с ID {dto.PublicationTypeId} не найден.");
        var newBook = mapper.Map<Book>(dto);
        newBook.Publisher = publisher;
        newBook.PublicationType = type;
        await bookRepository.Create(newBook);

        return mapper.Map<BookGetDto>(newBook);
    }

    /// <summary>
    /// Получение информации о книге по Id.
    /// </summary>
    /// <param name="dtoId"> Id книги </param>
    /// <returns>DTO книги</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если книга с указанным Id не найден.</exception>
    public async Task<BookGetDto> Get(int dtoId)
    {
        var book =await bookRepository.Read(dtoId)
            ?? throw new InvalidOperationException($"Книга с ID {dtoId} не найдена.");

        return mapper.Map<BookGetDto>(book);
    }

    /// <summary>
    /// Получает список с DTO всех книг.
    /// </summary>
    /// <returns>Список DTO всех записей</returns>
    public async Task<List<BookGetDto>> GetAll()
    {
        var books =await bookRepository.ReadAll();
        return mapper.Map<List<BookGetDto>>(books);
    }

    /// <summary>
    /// Обновляет информацию о книге по Id.
    /// </summary>
    /// <param name="dto">DTO с данными для обновления</param>
    /// <param name="dtoId">Id обновляемой зкниги</param>
    /// <returns>DTO обновленной книги</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если книга, издательство или тип издания с указанным Id не найдены.</exception>
    public async Task<BookGetDto> Update(BookCreateDto dto, int dtoId)
    {
        var bookToUpdate =await bookRepository.Read(dtoId) ?? throw new InvalidOperationException($"Книга с ID {dtoId} не найдена для обновления.");
        var publisher = await publisherRepository.Read(dto.PublisherId) ?? throw new InvalidOperationException($"Издательство с ID {dto.PublisherId} не найдено.");
        var type = await publicationTypeRepository.Read(dto.PublicationTypeId) ?? throw new InvalidOperationException($"Тип издания с ID {dto.PublicationTypeId} не найден.");
        mapper.Map(dto, bookToUpdate);

        bookToUpdate.Publisher = publisher;
        bookToUpdate.PublicationType = type;
        await bookRepository.Update(bookToUpdate);

        return mapper.Map<BookGetDto>(bookToUpdate);
    }

    /// <summary>
    /// Удаляет запись о книге по Id.
    /// </summary>
    /// <param name="dtoId">Id удаляемой книги/param>
    /// <exception cref="InvalidOperationException">Вызывается, если книга с указанным Id не найдена.</exception>
    public async Task Delete(int dtoId)
    {
        if (!await bookRepository.Delete(dtoId))
            throw new InvalidOperationException($"Книга с ID {dtoId} не найдена.");
    }
}
