namespace Library.Domain.Models;

public class Author
{
    public required int Id { get; set; }
    public required string FullName { get; set; }

    public List<Book> Books { get; set; } = new List<Book>();

    public override string ToString() => $"{FullName} ({Books.Count} книг)";
}
