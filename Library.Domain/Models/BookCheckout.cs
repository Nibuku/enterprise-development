namespace Library.Domain.Models;

/// <summary>
/// Класс для записи выданных книг
/// </summary>
public class BookCheckout
{
    /// <summary>
    /// Id записи о выдаче
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Дата взятия книги
    /// </summary>
    public required DateOnly LoanDate { get; set; }

    /// <summary>
    /// Количество дней
    /// </summary>
    public required int LoanDays { get; set; }

    /// <summary>
    /// Книга
    /// </summary>
    public required Book Book { get; set; }

    /// <summary>
    /// Читатель, взявший книгу
    /// </summary>
    public required BookReader Reader { get; set; }

    /// <summary>
    /// Дата возврата книги
    /// </summary>
    public DateOnly DueDate => LoanDate.AddDays(LoanDays);
}