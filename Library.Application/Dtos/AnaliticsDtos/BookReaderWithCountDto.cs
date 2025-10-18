namespace Library.Application.Dtos.AnaliticsDtos;

/// <summary>
/// DTO для аналитических запросов, связанных с читателем
/// </summary>
public class BookReaderWithCountDto
{
    /// <summary>
    /// Id читателя
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Полное имя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Дата регистрации читателя.
    /// </summary>
    public required DateOnly RegistrationDate { get; set; }

    /// <summary>
    /// Общее количество книг, взятых читателем.
    /// </summary>
    public required int Count { get; set; } = 0;
}
