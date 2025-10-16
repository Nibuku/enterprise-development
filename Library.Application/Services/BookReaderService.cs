using AutoMapper;
using Library.Application.Dtos;
using Library.Domain.Models;
using Library.Infrastructure.Repositories;


namespace Library.Application.Services;
public class BookReaderService(
    BookReaderRepository bookReaderRepository,
    IMapper mapper) : IApplicationService<BookReaderGetDto, BookReaderCreateDto, int>
{
    public BookReaderGetDto Create(BookReaderCreateDto dto)
    {
        var bookReader = mapper.Map<BookReader>(dto);
        bookReaderRepository.Create(bookReader);

        return mapper.Map<BookReaderGetDto>(bookReader);
    }

    public BookReaderGetDto Get(int dtoId)
    {
        var bookReader = bookReaderRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Читатель с ID {dtoId} не найден.");

        return mapper.Map<BookReaderGetDto>(bookReader);
    }

    public List<BookReaderGetDto> GetAll()
    {
        var bookReaders = bookReaderRepository.ReadAll();
        return mapper.Map<List<BookReaderGetDto>>(bookReaders);
    }

    public BookReaderGetDto Update(BookReaderCreateDto dto, int dtoId)
    {
        var readerToUpdate = bookReaderRepository.Read(dtoId) ?? throw new KeyNotFoundException($"Читатель с ID {dtoId} не найден.");
        mapper.Map(dto, readerToUpdate);
        bookReaderRepository.Update(readerToUpdate);
        return mapper.Map<BookReaderGetDto>(readerToUpdate);
    }

    public void Delete(int dtoId)
    {
        bookReaderRepository.Delete(dtoId);
    }
}
