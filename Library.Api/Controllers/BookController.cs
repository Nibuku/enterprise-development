using Library.Application.Dtos;
using Library.Application.Services;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для книг.
/// </summary>
/// <param name="bookService">Сервис для работы с книгами.</param>
/// <param name="logger">Логгер для записи информации.</param>
public class BookController(BookService bookService, ILogger<BookController> logger)
    : CrudControllerBase<BookGetDto, BookCreateDto, int>(bookService, logger);