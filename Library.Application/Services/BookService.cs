using AutoMapper;
using Library.Application.Dtos;
using Library.Application.Interfaces;
using Library.Domain.Models;
using Library.Infrastructure.Repositories;

namespace Library.Application.Services;

/// <summary>
/// Сервис, обеспечивающий CRUD-операции для работы с книгами.
/// </summary>
public class BookService(
    BookRepository bookRepository,
    PublisherRepository publisherRepository,
    PublicationTypeRepository publicationTypeRepository,
    IMapper mapper) : IApplicationService<BookGetDto, BookCreateDto, int>
{
    /// <summary>
    /// Вносит запись о новой книге.
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>DTO созданной книги</returns>
    public BookGetDto Create(BookCreateDto dto)
    {
        var publisher = publisherRepository.Read(dto.PublisherId) ?? throw new KeyNotFoundException($"Издательство с ID {dto.PublisherId} не найдено.");
        var type = publicationTypeRepository.Read(dto.PublicationTypeId) ?? throw new KeyNotFoundException($"Тип издания с ID {dto.PublicationTypeId} не найден.");
        var newBook = mapper.Map<Book>(dto);
        newBook.Publisher = publisher;
        newBook.PublicationType = type;
        bookRepository.Create(newBook);

        return mapper.Map<BookGetDto>(newBook);
    }

    /// <summary>
    /// Получение информации о книге по Id.
    /// </summary>
    /// <param name="dtoId"> Id книги </param>
    /// <returns>DTO книги</returns>
    /// <exception cref="KeyNotFoundException">Вызывается, если книга с указанным Id не найден.</exception>
    public BookGetDto Get(int dtoId)
    {
        var book = bookRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Книга с ID {dtoId} не найдена.");

        return mapper.Map<BookGetDto>(book);
    }

    /// <summary>
    /// Получает список с DTO всех книг.
    /// </summary>
    /// <returns>Список DTO всех записей</returns>
    public List<BookGetDto> GetAll()
    {
        var books = bookRepository.ReadAll();
        return mapper.Map<List<BookGetDto>>(books);
    }

    /// <summary>
    /// Обновляет информацию о книге по Id.
    /// </summary>
    /// <param name="dto">DTO с данными для обновления</param>
    /// <param name="dtoId">Id обновляемой зкниги</param>
    /// <returns>DTO обновленной книги</returns>
    /// <exception cref="KeyNotFoundException">Вызывается, если книга, издательство или тип издания с указанным Id не найдены.</exception>
    public BookGetDto Update(BookCreateDto dto, int dtoId)
    {
        var bookToUpdate = bookRepository.Read(dtoId) ?? throw new KeyNotFoundException($"Книга с ID {dtoId} не найдена для обновления.");
        var publisher = publisherRepository.Read(dto.PublisherId) ?? throw new KeyNotFoundException($"Издательство с ID {dto.PublisherId} не найдено.");
        var type = publicationTypeRepository.Read(dto.PublicationTypeId) ?? throw new KeyNotFoundException($"Тип издания с ID {dto.PublicationTypeId} не найден.");
        mapper.Map(dto, bookToUpdate);

        bookToUpdate.Publisher = publisher;
        bookToUpdate.PublicationType = type;
        bookRepository.Update(bookToUpdate);

        return mapper.Map<BookGetDto>(bookToUpdate);
    }

    /// <summary>
    /// Удаляет запись о книге по Id.
    /// </summary>
    /// <param name="dtoId">Id удаляемой книги/param>
    public void Delete(int dtoId)
    {
        bookRepository.Delete(dtoId);
    }
}
