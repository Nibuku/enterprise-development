namespace Library.Domain.Models;

public class Reader
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Address { get; set; }
    public string? Phone { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    public List<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
}