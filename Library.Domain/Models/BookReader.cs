namespace Library.Domain.Models;

/// <summary>
/// Класс для читателя
/// </summary>
public class BookReader
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
    /// Дата регистрации.
    /// </summary>
    public required DateOnly RegistrationDate { get; set; }
}