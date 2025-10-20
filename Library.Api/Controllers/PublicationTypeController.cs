using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для типов публикаций.
/// </summary>
/// <param name="publicationTypeService">Сервис для работы с типами.</param>
/// <param name="logger">Логгер для записи информации.</param>
public class PublicationTypeController(IApplicationService<PublicationTypeGetDto, PublicationTypeCreateDto, int> publicationTypeService, ILogger<PublicationTypeController> logger)
    : CrudControllerBase<PublicationTypeGetDto, PublicationTypeCreateDto, int>(publicationTypeService, logger);