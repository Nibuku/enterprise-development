using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Models;
using Library.Infrastructure.Repositories;

namespace Library.Application.Services;
public class BookService(
    BookRepository bookRepository,
    PublisherRepository publisherRepository,
    PublicationTypeRepository publicationTypeRepository,
    IMapper mapper) : IApplicationService<BookGetDto, BookCreateDto, int>
{
    public BookGetDto Create(BookCreateDto dto)
    {
        var publisher = publisherRepository.Read(dto.Publisher.Id) ?? throw new KeyNotFoundException($"Издательство с ID {dto.Publisher.Id} не найдено.");

        var type = publicationTypeRepository.Read(dto.PublicationType.Id) ?? throw new KeyNotFoundException($"Тип издания с ID {dto.PublicationType.Id} не найден.");

        var newBook = mapper.Map<Book>(dto);
        newBook.Publisher = publisher;
        newBook.PublicationType = type;
        bookRepository.Create(newBook);

        return mapper.Map<BookGetDto>(newBook);
    }

    public BookGetDto Get(int dtoId)
    {
        var book = bookRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Книга с ID {dtoId} не найдена.");

        return mapper.Map<BookGetDto>(book);
    }

    public List<BookGetDto> GetAll()
    {
        var books = bookRepository.ReadAll();
        return mapper.Map<List<BookGetDto>>(books);
    }

    public BookGetDto Update(BookCreateDto dto, int dtoId)
    {
        var bookToUpdate = bookRepository.Read(dtoId) ?? throw new KeyNotFoundException($"Книга с ID {dtoId} не найдена для обновления.");
        var publisher = publisherRepository.Read(dto.Publisher.Id) ?? throw new KeyNotFoundException($"Издательство с ID {dto.Publisher.Id} не найдено.");

        var type = publicationTypeRepository.Read(dto.PublicationType.Id) ?? throw new KeyNotFoundException($"Тип издания с ID {dto.PublicationType.Id} не найден.");
        mapper.Map(dto, bookToUpdate);

        bookToUpdate.Id = dtoId;
        bookToUpdate.Publisher = publisher;
        bookToUpdate.PublicationType = type;

        bookRepository.Update(bookToUpdate);

        return mapper.Map<BookGetDto>(bookToUpdate);
    }

    public void Delete(int dtoId)
    {
        bookRepository.Delete(dtoId);
    }
}
