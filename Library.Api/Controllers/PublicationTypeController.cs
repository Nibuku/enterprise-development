using Library.Application.Dtos;
using Library.Application.Services;

namespace Library.Api.Controllers;

public class PublicationTypeController(IApplicationService<PublicationTypeGetDto, PublicationTypeCreateDto, int> crudService, ILogger<PublicationTypeController> logger)
    : CrudControllerBase<PublicationTypeGetDto, PublicationTypeCreateDto, int>(crudService, logger);