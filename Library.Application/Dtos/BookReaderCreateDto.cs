namespace Library.Application.Dtos;
public class BookReaderCreateDto
{
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
}
