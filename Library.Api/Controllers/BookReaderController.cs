using Library.Application.Dtos;
using Library.Application.Services;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для читателей.
/// </summary>
/// <param name="readerService">Сервис для работы с читателями.</param>
/// <param name="logger">Логгер для записи информации.</param>
public class BookReaderController(BookReaderService readerService, ILogger<BookReaderController> logger)
    : CrudControllerBase<BookReaderGetDto, BookReaderCreateDto, int>(readerService, logger);