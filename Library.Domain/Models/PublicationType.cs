namespace Library.Domain.Models;

/// <summary>
/// Сlass for publication types
/// </summary>
public class PublicationType
{
    /// <summary>
    /// The unique id for type of publication.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Type of publication
    /// </summary>
    public required string Type { get; set; } 
}
