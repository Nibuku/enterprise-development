using Library.Domain.Enums;
using Library.Domain.Models;

namespace Library.Tests;

/// <summary>
/// Static methods to create and seed sample data for tests.
/// </summary>
public class DataSeed
{
    /// <summary>
    /// Initializes the DataSeed class by linking authors to books and creating book loans.
    /// </summary>
    public List<Author> Authors { get; } = GetAuthors();
    public List<Book> Books { get; } = GetBooks();
    public List<Reader> Readers { get; } = GetReaders();
    public List<BookLoan> Loans { get; }

    public DataSeed()
    {
        LinkAuthorsAndBooks(Authors, Books);
        Loans = GetBookLoans(Books, Readers);
    }
    private static void LinkAuthor(Author author, Book book)
    {
        if (!author.Books.Contains(book))
            author.Books.Add(book);

        if (!book.Authors.Contains(author))
            book.Authors.Add(author);
    }

    /// <summary>
    /// Creates a list of authors.
    /// </summary>
    /// <returns>A list of Author objects.</returns>
    public static List<Author> GetAuthors() =>
        [
            new Author { Id = 1, FullName = "Agatha Christie" },
            new Author { Id = 2, FullName = "Leo Tolstoy" },
            new Author { Id = 3, FullName = "Mikhail Bulgakov" },
            new Author { Id = 4, FullName = "Arkady & Boris Strugatsky" },
            new Author { Id = 5, FullName = "Editorial Board" },
            new Author { Id = 6, FullName = "Haruki Murakami" },
            new Author { Id = 7, FullName = "Dan Brown" },
            new Author { Id = 8, FullName = "Various Authors" },
            new Author { Id = 9, FullName = "Unknown Authors" },
            new Author { Id = 10, FullName = "John Smith" },
            new Author { Id = 11, FullName = "Alex Kozlov" },
            new Author { Id = 12, FullName = "Fyodor Dostoevsky" },
            new Author { Id = 13, FullName = "Ivan Petrov" }
        ];

    /// <summary>
    /// Creates a list of books.
    /// </summary>
    /// <returns>A list of Book objects.</returns>
    public static List<Book> GetBooks() =>
        [
            new Book { Id = 1, InventoryNumber = "INV-001", CatalogCode = "FIC-CHR-001", Title = "Murder on the Orient Express", PublicationType = PublicationType.Novel, Publisher = Publisher.AST, PublicationYear = 1934 },
            new Book { Id = 2, InventoryNumber = "INV-002", CatalogCode = "FIC-CHR-002", Title = "And Then There Were None", PublicationType = PublicationType.Novel, Publisher = Publisher.Eksmo, PublicationYear = 1939 },
            new Book { Id = 3, InventoryNumber = "INV-003", CatalogCode = "FIC-BRW-001", Title = "The Da Vinci Code", PublicationType = PublicationType.Novel, Publisher = Publisher.AST, PublicationYear = 2003 },
            new Book { Id = 4, InventoryNumber = "INV-004", CatalogCode = "FIC-BRW-002", Title = "Angels & Demons", PublicationType = PublicationType.Novel, Publisher = Publisher.Eksmo, PublicationYear = 2000 },
            new Book { Id = 5, InventoryNumber = "INV-005", CatalogCode = "FIC-BRW-003", Title = "Inferno", PublicationType = PublicationType.Novel, Publisher = Publisher.AST, PublicationYear = 2013 },
            new Book { Id = 6, InventoryNumber = "INV-006", CatalogCode = "FIC-STR-001", Title = "Roadside Picnic", PublicationType = PublicationType.Novel, Publisher = Publisher.Eksmo, PublicationYear = 1972 },
            new Book { Id = 7, InventoryNumber = "INV-007", CatalogCode = "FIC-TOL-001", Title = "War and Peace", PublicationType = PublicationType.Novel, Publisher = Publisher.AST, PublicationYear = 1869 },
            new Book { Id = 8, InventoryNumber = "INV-008", CatalogCode = "FIC-BUL-001", Title = "The Master and Margarita", PublicationType = PublicationType.Novel, Publisher = Publisher.Eksmo, PublicationYear = 1967 },
            new Book { Id = 9, InventoryNumber = "INV-009", CatalogCode = "SCI-001", Title = "Physics for Universities", PublicationType = PublicationType.Textbook, Publisher = Publisher.Piter, PublicationYear = 2023 },
            new Book { Id = 10, InventoryNumber = "INV-010", CatalogCode = "SCI-002", Title = "Norwegian Wood", PublicationType = PublicationType.Textbook, Publisher = Publisher.Prosveshchenie, PublicationYear = 2022 },
            new Book { Id = 11, InventoryNumber = "INV-011", CatalogCode = "REF-001", Title = "The Great Encyclopedia", PublicationType = PublicationType.ReferenceBook, Publisher = Publisher.AST, PublicationYear = 2020 },
            new Book { Id = 12, InventoryNumber = "INV-012", CatalogCode = "TUT-001", Title = "Programming in C#", PublicationType = PublicationType.Tutorial, Publisher = Publisher.DMKPress, PublicationYear = 2024 }
         ];

    /// <summary>
    /// Creates a list of readers.
    /// </summary>
    /// <returns>A list of Reader objects.</returns>
    public static List<Reader> GetReaders() =>
    [
        new Reader { Id = 1, FullName = "Elon Musk", Address = "Sun St. 1, Apt 1", Phone = "+79160000001", RegistrationDate = new DateOnly(2023, 1, 10) },
        new Reader { Id = 2, FullName = "Beyonce Knowles", Address = "Forest Ave 22, Apt 2", Phone = "+79160000002", RegistrationDate = new DateOnly(2023, 2, 15) },
        new Reader { Id = 3, FullName = "John Lennon", Address = "Gagarin St. 7, Apt 3", Phone = "+79160000003", RegistrationDate = new DateOnly(2023, 3, 20) },
        new Reader { Id = 4, FullName = "Angela Merkel", Address = "Soviet St. 10, Apt 4", Phone = "+79160000004", RegistrationDate = new DateOnly(2023, 4, 5) },
        new Reader { Id = 5, FullName = "Leonardo DiCaprio", Address = "Peace Ave. 8, Apt 5", Phone = "+79160000005", RegistrationDate = new DateOnly(2023, 5, 12) },
        new Reader { Id = 6, FullName = "Taylor Swift", Address = "Flower St. 15, Apt 6", Phone = "+79160000006", RegistrationDate = new DateOnly(2023, 6, 8) },
        new Reader { Id = 7, FullName = "Bill Gates", Address = "Forest St. 21, Apt 7", Phone = "+79160000007", RegistrationDate = new DateOnly(2023, 7, 25) },
        new Reader { Id = 8, FullName = "Oprah Winfrey", Address = "Cosmonauts Ave. 11, Apt 8", Phone = "+79160000008", RegistrationDate = new DateOnly(2023, 8, 14) },
        new Reader { Id = 9, FullName = "Steve Jobs", Address = "Central St. 9, Apt 9", Phone = "+79160000009", RegistrationDate = new DateOnly(2023, 9, 30) },
        new Reader { Id = 10, FullName = "Ariana Grande", Address = "Youth St. 13, Apt 10", Phone = "+79160000010", RegistrationDate = new DateOnly(2023, 10, 22) },
        new Reader { Id = 11, FullName = "Sergey Brin", Address = "Builders Ave. 4, Apt 11", Phone = "+79160000011", RegistrationDate = new DateOnly(2023, 11, 18) },
        new Reader { Id = 12, FullName = "Ellen DeGeneres", Address = "School St. 6, Apt 12", Phone = "+79160000012", RegistrationDate = new DateOnly(2023, 12, 5) }
    ];

    private static void LinkAuthorsAndBooks(List<Author> authors, List<Book> books)
    {
        LinkAuthor(authors[0], books[0]); 
        LinkAuthor(authors[0], books[1]);

        LinkAuthor(authors[6], books[2]); 
        LinkAuthor(authors[6], books[3]);
        LinkAuthor(authors[6], books[4]);

        LinkAuthor(authors[3], books[5]); 
        LinkAuthor(authors[1], books[6]); 
        LinkAuthor(authors[2], books[7]);
        LinkAuthor(authors[12], books[8]); 
        LinkAuthor(authors[5], books[9]); 
        LinkAuthor(authors[9], books[11]); 
        LinkAuthor(authors[4], books[10]);
    }

    /// <summary>
    /// Creates a list of book loans based on books and readers.
    /// </summary>
    /// <param name="books"> list of books to loan.</param>
    /// <param name="readers"> list of readers who will take books.</param>
    /// <returns>A list of BookLoan objects.</returns>
    public static List<BookLoan> GetBookLoans(List<Book> books, List<Reader> readers)
    {
        var loans = new List<BookLoan>
        {
        new() { Id = 1, Book = books[0], Reader = readers[0], LoanDate = new DateOnly(2025, 1, 10), LoanDays = 14 },
        new() { Id = 2, Book = books[1], Reader = readers[1], LoanDate = new DateOnly(2025, 2, 15), LoanDays = 21 },
        new() { Id = 3, Book = books[2], Reader = readers[2], LoanDate = new DateOnly(2025, 3, 1), LoanDays = 7 },
        new() { Id = 4, Book = books[3], Reader = readers[3], LoanDate = new DateOnly(2025, 2, 10), LoanDays = 30 },
        new() { Id = 5, Book = books[4], Reader = readers[4], LoanDate = new DateOnly(2025, 3, 5), LoanDays = 14 },
        new() { Id = 6, Book = books[5], Reader = readers[5], LoanDate = new DateOnly(2025, 3, 15), LoanDays = 21 },
        new() { Id = 7, Book = books[6], Reader = readers[6], LoanDate = new DateOnly(2025, 4, 1), LoanDays = 30 },
        new() { Id = 8, Book = books[7], Reader = readers[7], LoanDate = new DateOnly(2025, 4, 12), LoanDays = 10 },
        new() { Id = 9, Book = books[8], Reader = readers[8], LoanDate = new DateOnly(2025, 5, 3), LoanDays = 14 },
        new() { Id = 10, Book = books[9], Reader = readers[9], LoanDate = new DateOnly(2025, 5, 20), LoanDays = 21 },
        new() { Id = 11, Book = books[10], Reader = readers[10], LoanDate = new DateOnly(2025, 6, 7), LoanDays = 60 },
        new() { Id = 12, Book = books[11], Reader = readers[11], LoanDate = new DateOnly(2025, 6, 15), LoanDays = 14 },
        
        new() { Id = 13, Book = books[0], Reader = readers[1], LoanDate = new DateOnly(2025, 7, 1), LoanDays = 21 },
        new() { Id = 14, Book = books[2], Reader = readers[0], LoanDate = new DateOnly(2025, 7, 10), LoanDays = 14 },
        new() { Id = 15, Book = books[4], Reader = readers[2], LoanDate = new DateOnly(2025, 8, 5), LoanDays = 30 },
        new() { Id = 16, Book = books[9], Reader = readers[2], LoanDate = new DateOnly(2025, 6, 5), LoanDays = 10 },
        new() { Id = 17, Book = books[2], Reader = readers[4], LoanDate = new DateOnly(2025, 9, 5), LoanDays = 30 }
        };

        foreach (var loan in loans)
            loan.Reader.BookLoans.Add(loan);

        return loans;
    }
}
