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
        var book = bookRepository.Read(dto.BookId)?? throw new KeyNotFoundException($"Книга с ID {dto.BookId} не найдена.");
        var reader = readerRepository.Read(dto.ReaderId)?? throw new KeyNotFoundException($"Читатель с ID {dto.ReaderId} не найден.");
        var newCheckout = new BookCheckout
        {
            Id = 0,
            LoanDate = dto.LoanDate,
            LoanDays = dto.LoanDays,
            Book = book,
            Reader = reader
        };

        bookCheckoutRepository.Create(newCheckout);
        return mapper.Map<CheckoutGetDto>(newCheckout);
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
        var book = bookRepository.Read(dto.BookId)
               ?? throw new KeyNotFoundException($"Книга с ID {dto.BookId} не найдена.");

        var reader = readerRepository.Read(dto.ReaderId)
                     ?? throw new KeyNotFoundException($"Читатель с ID {dto.ReaderId} не найден.");
        checkoutToUpdate.Reader =reader;
        checkoutToUpdate.Book = book;
        mapper.Map(dto, checkoutToUpdate);
        bookCheckoutRepository.Update(checkoutToUpdate);
        return mapper.Map<CheckoutGetDto>(checkoutToUpdate);
    }

    public void Delete(int dtoId)
    {
        bookCheckoutRepository.Delete(dtoId);
    }
}
