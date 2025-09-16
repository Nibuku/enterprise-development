namespace Library.Domain.Models;

/// <summary>
/// Class for author model
/// </summary>
public class Author
{
    /// <summary>
    /// Gets or sets unique id for author
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Gets or sets full name of author.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets list of books written by this author.
    /// </summary>
    public List<Book> Books { get; set; } = [];
}
