namespace Library.Application.Contracts.Dtos;

/// <summary>
/// DTO для создания книг в библиотеке.
/// </summary>
public class BookCreateDto
{
    /// <summary>
    /// Инвентарный номер книги.
    /// </summary> 
    public required string InventoryNumber { get; set; }

    /// <summary>
    /// Код каталога.
    /// </summary>     
    public required string CatalogCode { get; set; }

    /// <summary>
    /// Название книги.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Id типа публикации
    /// </summary>
    public required int PublicationTypeId { get; set; }

    /// <summary>
    /// Id издательства
    /// </summary>
    public required int PublisherId { get; set; }

    /// <summary>
    /// Год издания
    /// </summary>
    public required int PublicationYear { get; set; }

    /// <summary>
    /// Список авторов
    /// </summary>
    public List<string> Authors { get; set; } = [];
}
