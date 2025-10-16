using Library.Application.Dtos;
using Library.Application.Services;

namespace Library.Api.Controllers;

public class BookReaderController(IApplicationService<BookReaderGetDto, BookReaderCreateDto, int> crudService, ILogger<BookReaderController> logger)
    : CrudControllerBase<BookReaderGetDto, BookReaderCreateDto, int>(crudService, logger);