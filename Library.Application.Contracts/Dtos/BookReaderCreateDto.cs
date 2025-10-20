namespace Library.Application.Contracts.Dtos;

/// <summary>
/// DTO для создания читателя
/// </summary>
public class BookReaderCreateDto
{
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
    /// Дата регистрации.
    /// </summary>
    public required DateOnly RegistrationDate { get; set; }
}
