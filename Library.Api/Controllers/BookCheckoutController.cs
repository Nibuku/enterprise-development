using Library.Application.Contracts.Dtos;
using Library.Application.Contracts.Interfaces;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для записей о выдаче.
/// </summary>
/// <param name="checkoutService">Сервис, работающий с записями о выдаче.</param>
/// <param name="logger">Логгер для записи информации.</param>
public class BookCheckoutController(IBookCheckoutService checkoutService, ILogger<BookCheckoutController> logger)
    : CrudControllerBase<CheckoutGetDto, CheckoutCreateDto, int>(checkoutService, logger);