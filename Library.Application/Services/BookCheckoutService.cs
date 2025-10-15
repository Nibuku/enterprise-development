using AutoMapper;
using Library.Application.Dtos;
using Library.Infrastructure.Repositories;
using Library.Domain.Models;

namespace Library.Application.Services;
public class BookCheckoutService(
    BookCheckoutRepository bookCheckoutRepository,
    BookRepository bookRepository,BookReaderRepository readerRepository,
    IMapper mapper) : IApplicationService<CheckoutGetDto, CheckoutCreateDto, int>
{

    public CheckoutGetDto Create(CheckoutCreateDto dto)
    {
        var book = bookRepository.Read(dto.Book.Id)?? throw new KeyNotFoundException($"Книга с ID {dto.Book.Id} не найдена.");

        var reader = readerRepository.Read(dto.Reader.Id)?? throw new KeyNotFoundException($"Читатель с ID {dto.Reader.Id} не найден.");
        var bookCheckout = mapper.Map<BookCheckout>(dto);
        bookCheckoutRepository.Create(bookCheckout);

        return mapper.Map<CheckoutGetDto>(bookCheckout);
    }

    public CheckoutGetDto Get(int dtoId)
    {
        var bookCheckout = bookCheckoutRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Выдача с ID {dtoId} не найдена.");

        return mapper.Map<CheckoutGetDto>(bookCheckout);
    }

    public List<CheckoutGetDto> GetAll()
    {
        var bookCheckout = bookCheckoutRepository.ReadAll();
        return mapper.Map<List<CheckoutGetDto>>(bookCheckout);
    }

    public CheckoutGetDto Update(CheckoutCreateDto dto, int dtoId)
    {
        var checkoutToUpdate = bookCheckoutRepository.Read(dtoId) ?? throw new KeyNotFoundException($"Выдача с ID {dtoId} не найдена.");
        mapper.Map(dto, checkoutToUpdate);
        checkoutToUpdate.Id = dtoId;
        bookCheckoutRepository.Update(checkoutToUpdate);
        return mapper.Map<CheckoutGetDto>(checkoutToUpdate);
    }

    public void Delete(int dtoId)
    {
        bookCheckoutRepository.Delete(dtoId);
    }
}
