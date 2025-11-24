using Library.Application.Contracts.Dtos;

namespace Library.Application.Contracts.Interfaces;
public interface IBookCheckoutService: IApplicationService<CheckoutGetDto, CheckoutCreateDto, int>
{
    /// <summary>
    /// Метод для получения коллекции контрактов (DTO)
    /// </summary>
    /// <param name="contracts">Коллекция контрактов</param>
   public Task ReceiveContract(IList<CheckoutCreateDto> contracts);
}
