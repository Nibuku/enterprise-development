using Library.Domain.Enums;

namespace Library.Domain.Models;

/// <summary>
/// Class for book model in library.
/// </summary>
public class Book
{
    /// <summary>
    /// Gets or sets unique id for the book.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets unique inventory number of the book.
    /// </summary>
    public required string InventoryNumber { get; set; }

    /// <summary>
    /// Gets or sets catalog code for the book.
    /// </summary>
    public required string CatalogCode { get; set; }

    /// <summary>
    /// Gets or sets title of the book.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets type of publication
    /// </summary>
    public required PublicationType PublicationType { get; set; }

    /// <summary>
    /// Gets or sets publisher of the book.
    /// </summary>
    public required Publisher Publisher { get; set; }

    /// <summary>
    /// Gets or sets year when book was published.
    /// </summary>
    public required int PublicationYear { get; set; }

    /// <summary>
    /// Gets or sets a list of authors who wrote this book.
    /// </summary>
    public List<Author> Authors { get; set; } = new List<Author>();
}