using Library.Application.Dtos;
using Library.Application.Services;

namespace Library.Api.Controllers;

public class BookController(IApplicationService<BookGetDto, BookCreateDto, int> crudService, ILogger<BookController> logger)
    : CrudControllerBase<BookGetDto, BookCreateDto, int>(crudService, logger);