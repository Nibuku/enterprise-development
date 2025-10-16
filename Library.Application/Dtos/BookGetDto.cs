namespace Library.Application.Dtos;
public class BookGetDto
{
    /// <summary>
    /// The unique id for the book.
    /// </summary>
    public int Id { get; set; }

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
    public required int PublicationTypeId { get; set; }

    /// <summary>
    /// Publisher of the book.
    /// </summary>
    public required int PublisherId { get; set; }

    /// <summary>
    /// The year when book was published.
    /// </summary>
    public required int PublicationYear { get; set; }

    /// <summary>
    /// List of authors who wrote this book.
    /// </summary>
    public List<string> Authors { get; set; } = [];
}
