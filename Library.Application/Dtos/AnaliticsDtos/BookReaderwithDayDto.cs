namespace Library.Application.Dtos.AnaliticsDtos;
internal class BookReaderwithDayDto
{
    /// <summary>
    /// The unique id for the reader.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Full name.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Physical address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Phone number.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Date when the reader was registered in the library.
    /// </summary>
    public required DateOnly RegistrationDate { get; set; }

    public required int TotalDays { get; set; } = 0;
}
