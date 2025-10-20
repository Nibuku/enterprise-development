﻿using AutoMapper;
using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;
using Library.Domain.Interfaces;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис, обеспечивающий CRUD-операции для работы с записями о выдаче книг читателям.
/// </summary>
public class BookCheckoutService(
    IRepository<BookCheckout, int> bookCheckoutRepository,
    IRepository<Book, int> bookRepository, IRepository<BookReader, int> readerRepository,
    IMapper mapper) : IApplicationService<CheckoutGetDto, CheckoutCreateDto, int>
{
    /// <summary>
    /// Создает новую запись о выдаче.
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>DTO созданной записи</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если книга или читатель с указанным Id не найдены.</exception>
    public CheckoutGetDto Create(CheckoutCreateDto dto)
    {
        var book = bookRepository.Read(dto.BookId)?? throw new InvalidOperationException($"Книга с ID {dto.BookId} не найдена.");
        var reader = readerRepository.Read(dto.ReaderId)?? throw new InvalidOperationException($"Читатель с ID {dto.ReaderId} не найден.");
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

    /// <summary>
    /// Получение записи о выдаче книги по Id.
    /// </summary>
    /// <param name="dtoId"> Id записи</param>
    /// <returns>DTO найденной записи</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если запись с указанным Id не найдена.</exception>
    public CheckoutGetDto Get(int dtoId)
    {
        var bookCheckout = bookCheckoutRepository.Read(dtoId)
            ?? throw new InvalidOperationException($"Выдача с ID {dtoId} не найдена.");

        return mapper.Map<CheckoutGetDto>(bookCheckout);
    }

    /// <summary>
    /// Получает все записи о выдаче.
    /// </summary>
    /// <returns>Список DTO всех записей</returns>
    public List<CheckoutGetDto> GetAll()
    {
        var bookCheckout = bookCheckoutRepository.ReadAll();
        return mapper.Map<List<CheckoutGetDto>>(bookCheckout);
    }

    /// <summary>
    /// Обновляет существующую запись о выдаче по Id.
    /// </summary>
    /// <param name="dto">DTO с данными для обновления</param>
    /// <param name="dtoId">Id обновляемой записи</param>
    /// <returns>DTO обновленной записи о выдаче</returns>
    /// <exception cref="InvalidOperationException">Вызывается, если книга или читатель с указанным Id не найдены.</exception>
    public CheckoutGetDto Update(CheckoutCreateDto dto, int dtoId)
    {
        var checkoutToUpdate = bookCheckoutRepository.Read(dtoId) ?? throw new InvalidOperationException($"Выдача с ID {dtoId} не найдена.");
        var book = bookRepository.Read(dto.BookId)
               ?? throw new InvalidOperationException($"Книга с ID {dto.BookId} не найдена.");

        var reader = readerRepository.Read(dto.ReaderId)
                     ?? throw new InvalidOperationException($"Читатель с ID {dto.ReaderId} не найден.");
        checkoutToUpdate.Reader =reader;
        checkoutToUpdate.Book = book;
        mapper.Map(dto, checkoutToUpdate);
        bookCheckoutRepository.Update(checkoutToUpdate);
        return mapper.Map<CheckoutGetDto>(checkoutToUpdate);
    }

    /// <summary>
    /// Удаляет запись о выдаче книги по Id.
    /// </summary>
    /// <param name="dtoId">Id удаляемой записи</param>
    /// <exception cref="InvalidOperationException">Вызывается, если запись с указанным Id не найдены.</exception>
    public void Delete(int dtoId)
    {
        if (!bookCheckoutRepository.Delete(dtoId))
            throw new InvalidOperationException($"Выдача с ID {dtoId} не найдена.");
    }
}
