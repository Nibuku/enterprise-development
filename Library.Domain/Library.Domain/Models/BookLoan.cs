namespace Library.Domain.Models;

public class BookLoan
{
    public required int Id { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.UtcNow;
    public required int LoanDays { get; set; }
    public required int BookId { get; set; }
    public required int ReaderId { get; set; }

    public Book Book { get; set; } = null!;
    public Reader Reader { get; set; } = null!;

    public DateTime DueDate => LoanDate.AddDays(LoanDays);
}