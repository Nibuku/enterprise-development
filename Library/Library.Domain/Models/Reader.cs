namespace Library.Domain.Models;

/// <summary>
/// Class for a library's reader
/// </summary>
public class Reader
{
    /// <summary>
    /// Gets or sets the unique id for the reader.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets full name of the reader.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets physical address of the reader.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets phone number of the reader.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets date when the reader was registered in the library.
    /// </summary>
    public required DateOnly RegistrationDate { get; set; }

    /// <summary>
    /// Gets or sets a list of book loans associated with this reader.
    /// </summary>
    public List<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
}