using Library.Application.Dtos;
using Library.Application.Services;

namespace Library.Api.Controllers;

public class PublisherController(IApplicationService<PublisherGetDto, PublisherCreateDto, int> crudService, ILogger<PublisherController> logger)
    : CrudControllerBase<PublisherGetDto, PublisherCreateDto, int>(crudService, logger);