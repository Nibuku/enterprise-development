namespace Library.Domain.Models;

/// <summary>
/// Класс для издательств
/// </summary>
public class Publisher
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
