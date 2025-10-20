namespace Library.Application.Contracts.Dtos.AnaliticsDtos;

/// <summary>
/// Аналитическое DTO для издательств
/// </summary>
public class PublisherCountDto
{
    /// <summary>
    /// Название издательства
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Количество книг данного издательства в библиотеке
    /// </summary>
    public required int Count { get; set; } = 0;
}
