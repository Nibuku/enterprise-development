using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для книг.
/// </summary>
/// <param name="bookService">Сервис для работы с книгами.</param>
/// <param name="logger">Логгер для записи информации.</param>
public class BookController(IApplicationService<BookGetDto, BookCreateDto, int> bookService, ILogger<BookController> logger)
    : CrudControllerBase<BookGetDto, BookCreateDto, int>(bookService, logger);