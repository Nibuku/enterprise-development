namespace Library.Application.Contracts.Dtos;

/// <summary>
/// DTO для издательств
/// </summary>
public class PublisherGetDto
{
    /// <summary>
    /// Id издательства
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название издательства
    /// </summary>
    public required string Name { get; set; }
}
