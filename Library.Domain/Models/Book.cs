namespace Library.Domain.Models;

/// <summary>
/// Класс для книг в библиотеке.
/// </summary>
public class Book
{
    /// <summary>
    /// Id для книги.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Инвентарный номер книги.
    /// </summary>
    public required string InventoryNumber { get; set; }

    /// <summary>
    /// Код каталога.
    /// </summary>
    public required string CatalogCode { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Тип публикации
    /// </summary>
    public required PublicationType PublicationType { get; set; }

    /// <summary>
    /// Издательство
    /// </summary>
    public required Publisher Publisher { get; set; }

    /// <summary>
    /// Год издания
    /// </summary>
    public required int PublicationYear { get; set; }

    /// <summary>
    /// Список авторов
    /// </summary>
    public List<string> Authors { get; set; } = [];
}
