using Library.Domain.Models;

namespace Library.Domain.Data;

/// <summary>
/// Класс для создания и хранения данных для тестов
/// </summary>
public static class DataSeed
{
    /// <summary>
    /// Издательства
    /// </summary>
    public static readonly List<Publisher> Publishers;

    /// <summary>
    /// Типы публикаций
    /// </summary>
    public static readonly List<PublicationType> PublicationTypes;

    /// <summary>
    /// Список книг
    /// </summary>
    public static List<Book> Books { get; }

    /// <summary>
    /// Список читателей
    /// </summary>
    public static List<BookReader> Readers { get; }

    /// <summary>
    /// Выдачи
    /// </summary>
    public static List<BookCheckout> Checkouts { get; }

    /// <summary>
    /// Статический конструктор класса.
    /// </summary>
    static DataSeed()
    {
        Publishers =
        [
            new Publisher { Id = 1, Name = "AST" },
            new Publisher { Id = 2, Name = "Eksmo" },
            new Publisher { Id = 3, Name = "Piter" },
            new Publisher { Id = 4, Name = "Prosveshchenie" },
            new Publisher { Id = 5, Name = "DMKPress" }
        ];

        PublicationTypes =
        [
            new PublicationType { Id = 1, Type = "Novel" },
            new PublicationType { Id = 2, Type = "Textbook" },
            new PublicationType { Id = 3, Type = "ReferenceBook" },
            new PublicationType { Id = 4, Type = "Tutorial" }
        ];

        Readers = GetReaders();
        Books = GetBooks();
        Checkouts = GetBookCheckouts(Books, Readers);
    }

    /// <summary>
    /// Метод создает список книг.
    /// </summary>
    /// <returns> Список объектов типа Book. </returns>
    public static List<Book> GetBooks() =>
    [
        new Book { Id = 1, InventoryNumber = "INV-001", CatalogCode = "FIC-CHR-001", Title = "Murder on the Orient Express", Authors = ["Agatha Christie"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Novel"), Publisher =  Publishers.Single(p => p.Name == "AST"), PublicationYear = 1934 },
        new Book { Id = 2, InventoryNumber = "INV-002", CatalogCode = "FIC-CHR-002", Title = "And Then There Were None", Authors = ["Agatha Christie"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Novel"), Publisher = Publishers.Single(p => p.Name == "Eksmo"), PublicationYear = 1939 },
        new Book { Id = 3, InventoryNumber = "INV-003", CatalogCode = "FIC-BRW-001", Title = "The Da Vinci Code", Authors = ["Dan Brown"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Novel"), Publisher = Publishers.Single(p => p.Name == "AST"), PublicationYear = 2003 },
        new Book { Id = 4, InventoryNumber = "INV-004", CatalogCode = "FIC-BRW-002", Title = "Angels & Demons", Authors = ["Dan Brown"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Novel"), Publisher = Publishers.Single(p => p.Name == "Eksmo"), PublicationYear = 2000 },
        new Book { Id = 5, InventoryNumber = "INV-005", CatalogCode = "FIC-BRW-003", Title = "Inferno", Authors = ["Dan Brown"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Novel"), Publisher = Publishers.Single(p => p.Name == "AST"), PublicationYear = 2013 },
        new Book { Id = 6, InventoryNumber = "INV-006", CatalogCode = "FIC-STR-001", Title = "Roadside Picnic", Authors = ["Arkady Strugatsky", "Boris Strugatsky"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Novel"), Publisher = Publishers.Single(p => p.Name == "Eksmo"), PublicationYear = 1972 },
        new Book { Id = 7, InventoryNumber = "INV-007", CatalogCode = "FIC-TOL-001", Title = "War and Peace", Authors = ["Leo Tolstoy"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Novel"), Publisher = Publishers.Single(p => p.Name == "AST"), PublicationYear = 1869 },
        new Book { Id = 8, InventoryNumber = "INV-008", CatalogCode = "FIC-BUL-001", Title = "The Master and Margarita", Authors = ["Mikhail Bulgakov"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Novel"), Publisher = Publishers.Single(p => p.Name == "Eksmo"), PublicationYear = 1967 },
        new Book { Id = 9, InventoryNumber = "INV-009", CatalogCode = "SCI-001", Title = "Physics for Universities", Authors = ["Isaac Newton"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Textbook"), Publisher = Publishers.Single(p => p.Name == "Piter"), PublicationYear = 2023 },
        new Book { Id = 10, InventoryNumber = "INV-010", CatalogCode = "SCI-002", Title = "Norwegian Wood", Authors = ["Haruki Murakami"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Textbook"), Publisher = Publishers.Single(p => p.Name == "Prosveshchenie"), PublicationYear = 2022 },
        new Book { Id = 11, InventoryNumber = "INV-011", CatalogCode = "REF-001", Title = "The Great Encyclopedia", Authors = ["Plato"], PublicationType = PublicationTypes.Single(pt => pt.Type == "ReferenceBook"), Publisher = Publishers.Single(p => p.Name == "AST"), PublicationYear = 2020 },
        new Book { Id = 12, InventoryNumber = "INV-012", CatalogCode = "TUT-001", Title = "Programming in C#", Authors = ["Bill Gates"], PublicationType = PublicationTypes.Single(pt => pt.Type == "Tutorial"), Publisher = Publishers.Single(p => p.Name == "DMKPress"), PublicationYear = 2024 }
    ];

    /// <summary>
    /// Метод создает список читателей.
    /// </summary>
    /// <returns> Список объектов типа Book.</returns>
    public static List<BookReader> GetReaders() =>
    [
        new BookReader { Id = 1, FullName = "Elon Musk", Address = "Sun St. 1, Apt 1", Phone = "+79160000001", RegistrationDate = new DateOnly(2023, 1, 10) },
        new BookReader { Id = 2, FullName = "Beyonce Knowles", Address = "Forest Ave 22, Apt 2", Phone = "+79160000002", RegistrationDate = new DateOnly(2023, 2, 15) },
        new BookReader { Id = 3, FullName = "John Lennon", Address = "Gagarin St. 7, Apt 3", Phone = "+79160000003", RegistrationDate = new DateOnly(2023, 3, 20) },
        new BookReader { Id = 4, FullName = "Angela Merkel", Address = "Soviet St. 10, Apt 4", Phone = "+79160000004", RegistrationDate = new DateOnly(2023, 4, 5) },
        new BookReader { Id = 5, FullName = "Leonardo DiCaprio", Address = "Peace Ave. 8, Apt 5", Phone = "+79160000005", RegistrationDate = new DateOnly(2023, 5, 12) },
        new BookReader { Id = 6, FullName = "Taylor Swift", Address = "Flower St. 15, Apt 6", Phone = "+79160000006", RegistrationDate = new DateOnly(2023, 6, 8) },
        new BookReader { Id = 7, FullName = "Bill Gates", Address = "Forest St. 21, Apt 7", Phone = "+79160000007", RegistrationDate = new DateOnly(2023, 7, 25) },
        new BookReader { Id = 8, FullName = "Oprah Winfrey", Address = "Cosmonauts Ave. 11, Apt 8", Phone = "+79160000008", RegistrationDate = new DateOnly(2023, 8, 14) },
        new BookReader { Id = 9, FullName = "Steve Jobs", Address = "Central St. 9, Apt 9", Phone = "+79160000009", RegistrationDate = new DateOnly(2023, 9, 30) },
        new BookReader { Id = 10, FullName = "Ariana Grande", Address = "Youth St. 13, Apt 10", Phone = "+79160000010", RegistrationDate = new DateOnly(2023, 10, 22) },
        new BookReader { Id = 11, FullName = "Sergey Brin", Address = "Builders Ave. 4, Apt 11", Phone = "+79160000011", RegistrationDate = new DateOnly(2023, 11, 18) },
        new BookReader { Id = 12, FullName = "Ellen DeGeneres", Address = "School St. 6, Apt 12", Phone = "+79160000012", RegistrationDate = new DateOnly(2023, 12, 5) }
    ];

    /// <summary>
    /// Создает список выдач книг с указанием читателей и книг
    /// </summary>
    /// <param name="books"> список книг для выдачи</param>
    /// <param name="readers"> список читателей</param>
    /// <returns> Список объектов типо BookCheckout </returns>
    public static List<BookCheckout> GetBookCheckouts(List<Book> books, List<BookReader> readers) =>
    [
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

        new() { Id = 13, Book = books[0], Reader = readers[1], LoanDate = new DateOnly(2025, 10, 1), LoanDays = 21 },
        new() { Id = 14, Book = books[2], Reader = readers[0], LoanDate = new DateOnly(2025, 10, 10), LoanDays = 14 },
        new() { Id = 15, Book = books[4], Reader = readers[2], LoanDate = new DateOnly(2025, 10, 5), LoanDays = 30 },
        new() { Id = 16, Book = books[9], Reader = readers[2], LoanDate = new DateOnly(2025, 10, 5), LoanDays = 10 },
        new() { Id = 17, Book = books[2], Reader = readers[4], LoanDate = new DateOnly(2025, 10, 5), LoanDays = 30 }
    ];
}
