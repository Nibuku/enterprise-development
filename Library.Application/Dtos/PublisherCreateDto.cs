namespace Library.Application.Dtos;

/// <summary>
/// DTO для издательств
/// </summary>
public class PublisherCreateDto
{
    /// <summary>
    /// Название издательства
    /// </summary>
    public required string Name { get; set; }
}
