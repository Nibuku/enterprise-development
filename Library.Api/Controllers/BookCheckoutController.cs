using Library.Application.Dtos;
using Library.Application.Services;

namespace Library.Api.Controllers;

public class BookCheckoutController(IApplicationService<CheckoutGetDto, CheckoutCreateDto, int> crudService, ILogger<BookCheckoutController> logger)
    : CrudControllerBase<CheckoutGetDto, CheckoutCreateDto, int>(crudService, logger);