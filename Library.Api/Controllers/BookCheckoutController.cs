using Library.Application.Dtos;
using Library.Application.Services;

namespace Library.Api.Controllers;

/// <summary>
/// Предоставляет API-методы для записей о выдаче.
/// </summary>
/// <param name="checkoutService">Сервис, работающий с записями о выдаче.</param>
/// <param name="logger">Логгер для записи информации.</param>
public class BookCheckoutController(BookCheckoutService checkoutService, ILogger<BookCheckoutController> logger)
    : CrudControllerBase<CheckoutGetDto, CheckoutCreateDto, int>(checkoutService, logger);