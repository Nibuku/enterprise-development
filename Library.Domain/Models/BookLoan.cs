namespace Library.Domain.Models;

/// <summary>
/// Class for a book loan, tracking a book borrowed by a reader.
/// </summary>
public class BookLoan
{
    /// <summary>
    /// Gets or sets unique id for the loan.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Gets or sets date when the book was loaned.
    /// </summary>
    public required DateOnly LoanDate { get; set; }

    /// <summary>
    /// Gets or sets number of days the loan is for.
    /// </summary>
    public required int LoanDays { get; set; }

    /// <summary>
    /// Gets or sets the specific book that was loaned.
    /// </summary>
    public required Book Book { get; set; }

    /// <summary>
    /// Gets or sets the specific reader who took the book
    /// </summary>
    public required Reader Reader { get; set; }

    /// <summary>
    /// Gets the calculated due date of the loan.
    /// </summary>
    public DateOnly DueDate => LoanDate.AddDays(LoanDays);
}