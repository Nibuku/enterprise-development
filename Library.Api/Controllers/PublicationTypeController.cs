using Library.Application.Dtos;
using Library.Application.Services;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для типов публикаций.
/// </summary>
/// <param name="publicationTypeService">Сервис для работы с типами.</param>
/// <param name="logger">Логгер для записи информации.</param>
public class PublicationTypeController(PublicationTypeService publicationTypeService, ILogger<PublicationTypeController> logger)
    : CrudControllerBase<PublicationTypeGetDto, PublicationTypeCreateDto, int>(publicationTypeService, logger);