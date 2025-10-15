using Library.Domain.Models;

namespace Library.Application.Dtos;

public class BookCreateDto
{
    /// <summary>
    /// The unique inventory number of the book.
    /// </summary>
    public required string InventoryNumber { get; set; }

    /// <summary>
    /// Catalog code for the book.
    /// </summary>
    public required string CatalogCode { get; set; }

    /// <summary>
    /// Title of the book.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Type of publication
    /// </summary>
    public required PublicationType PublicationType { get; set; }

    /// <summary>
    /// Publisher of the book.
    /// </summary>
    public required Publisher Publisher { get; set; }

    /// <summary>
    /// The year when book was published.
    /// </summary>
    public required int PublicationYear { get; set; }

    /// <summary>
    /// List of authors who wrote this book.
    /// </summary>
    public List<string> Authors { get; set; } = [];
}
