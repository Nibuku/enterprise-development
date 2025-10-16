namespace Library.Application.Dtos;
public class CheckoutGetDto
{
    /// <summary>
    /// Unique id for the loan.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Date when the book was loaned.
    /// </summary>
    public required DateOnly LoanDate { get; set; }

    /// <summary>
    /// Number of days the loan is for.
    /// </summary>
    public required int LoanDays { get; set; }

    /// <summary>
    /// The specific book that was loaned.
    /// </summary>
    public required int BookId { get; set; }

    /// <summary>
    /// The specific reader who took the book
    /// </summary>
    public required int ReaderId { get; set; }

    /// <summary>
    /// The calculated due date of the loan.
    /// </summary>
    public DateOnly DueDate => LoanDate.AddDays(LoanDays);
}
