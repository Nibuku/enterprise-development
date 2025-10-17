namespace Library.Application.Dtos;

/// <summary>
/// DTO для создания записи о выданных книгах
/// </summary>
public class CheckoutCreateDto
{

    /// <summary>
    /// Дата взятия книги
    /// </summary>
    public required DateOnly LoanDate { get; set; }

    /// <summary>
    /// Количество дней
    /// </summary>
    public required int LoanDays { get; set; }

    /// <summary>
    /// Id книги
    /// </summary>
    public required int BookId { get; set; }

    /// <summary>
    /// Id читателя, взявшего книгу
    /// </summary>
    public required int ReaderId { get; set; }

    /// <summary>
    /// Дата возврата книги
    /// </summary>
    public DateOnly DueDate => LoanDate.AddDays(LoanDays);
}
