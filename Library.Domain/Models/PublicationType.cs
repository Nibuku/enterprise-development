namespace Library.Domain.Models;

/// <summary>
/// Класс для типов публикации
/// </summary>
public class PublicationType
{
    /// <summary>
    /// Id типа публикации
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Тип публикации
    /// </summary>
    public required string Type { get; set; } 
}
