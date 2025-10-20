using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для издательств.
/// </summary>
/// <param name="publisherService">Сервис для работы с издательствами.</param>
/// <param name="logger">Логгер для записи информации.</param>
public class PublisherController(IApplicationService<PublisherGetDto, PublisherCreateDto, int> publisherService, ILogger<PublisherController> logger)
    : CrudControllerBase<PublisherGetDto, PublisherCreateDto, int>(publisherService, logger);
