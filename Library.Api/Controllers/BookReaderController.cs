using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для читателей.
/// </summary>
/// <param name="readerService">Сервис для работы с читателями.</param>
/// <param name="logger">Логгер для записи информации.</param>
public class BookReaderController(IApplicationService<BookReaderGetDto, BookReaderCreateDto, int> readerService, ILogger<BookReaderController> logger)
    : CrudControllerBase<BookReaderGetDto, BookReaderCreateDto, int>(readerService, logger);