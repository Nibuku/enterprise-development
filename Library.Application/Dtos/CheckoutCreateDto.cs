using Library.Domain.Models;

namespace Library.Application.Dtos;
public class CheckoutCreateDto
{

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
    public required Book Book { get; set; }

    /// <summary>
    /// The specific reader who took the book
    /// </summary>
    public required BookReader Reader { get; set; }

    /// <summary>
    /// The calculated due date of the loan.
    /// </summary>
    public DateOnly DueDate => LoanDate.AddDays(LoanDays);
}
