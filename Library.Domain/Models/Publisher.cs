namespace Library.Domain.Models;

/// <summary>
/// A list of publishers.
/// </summary>
public class Publisher
{
    /// <summary>
    /// The unique id for publisher.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Publisher's name.
    /// </summary>
    public required string Name { get; set; } 
}
