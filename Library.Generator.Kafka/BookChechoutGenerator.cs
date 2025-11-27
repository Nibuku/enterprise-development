using Bogus;
using Library.Application.Contracts.Dtos;

namespace Library.Generator.Kafka;

/// <summary>
/// Генератор случайных данных для DTO выдачи книг.
/// </summary>
public static class BookCheckoutGenerator
{
    /// <summary>
    /// Генерирует список случайных DTO выдач книг.
    /// </summary>
    /// <param name="count">Количество DTO для генерации.</param>
    /// <returns>Список сгенерированных DTO 
    /// </returns>
    public static List<CheckoutCreateDto> GenerateLinks(int count) =>
        new Faker<CheckoutCreateDto>()
            .RuleFor(x => x.BookId, f => f.Random.Int(1, 11))
            .RuleFor(x => x.ReaderId, f => f.Random.Int(1, 11))
            .RuleFor(x => x.LoanDate, f =>
            {
                var dt = f.Date.Past(1);
                return new DateOnly(dt.Year, dt.Month, dt.Day);
            })
            .RuleFor(x => x.LoanDays, f => f.Random.Int(1, 30))
            .Generate(count);
}
