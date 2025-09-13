using Library.Domain.Enums;
using Library.Domain.Models;

namespace Library.Tests.Library.Tests;
public static class DataSeed
{
    private static void LinkAuthor(Author author, Book book)
    {
        if (!author.Books.Contains(book))
            author.Books.Add(book);

        if (!book.Authors.Contains(author))
            book.Authors.Add(author);
    }

    public static void LinkReadersWithLoans(List<Reader> readers, List<BookLoan> bookLoans)
    {
        foreach (var loan in bookLoans)
        {
            loan.Reader.BookLoans.Add(loan);
        }
    }
    public static List<Author> GetAuthors() =>
        [
        new() {Id=1, FullName = "Агата Кристи"},
        new() {Id=2, FullName = "Толстой Л.Н."},
        new() {Id=3, FullName = "Булгаков М.А."},
        new() {Id=4, FullName = "Братья Стругацкие"},
        new() { Id = 8, FullName = "редакция" },
        new() { Id = 6, FullName = "Харуки Мураками" },
        new() { Id = 7, FullName = "Дэн Браун" },
        new() { Id = 8, FullName = "коллектив авторов" },
        new() { Id = 9, FullName = "различные авторы" },
        new() { Id = 10, FullName = "Смит Дж." },
        new() { Id = 11, FullName = "Козлов А.П." },
        new() { Id = 12, FullName = "Достоевский Ф.М." },
        new() { Id = 13, FullName = "Петров И.И." }
        ];

    public static List<Book> GetBooks() =>
        [
        new() { Id = 1, InventoryNumber = "INV-001", CatalogCode = "FIC-CHR-001", Title = "Убийство в Восточном экспрессе", PublicationType = PublicationType.Novel, Publisher = Publisher.Eksmo, PublicationYear = 1934 },
        new() { Id = 2, InventoryNumber = "INV-002", CatalogCode = "FIC-CHR-002", Title = "Десять негритят", PublicationType = PublicationType.Novel, Publisher = Publisher.AST, PublicationYear = 1939 },
        new() { Id = 3, InventoryNumber = "INV-003", CatalogCode = "FIC-BRW-001", Title = "Код да Винчи", PublicationType = PublicationType.Novel, Publisher = Publisher.Eksmo, PublicationYear = 2003 },
        new() { Id = 4, InventoryNumber = "INV-004", CatalogCode = "FIC-BRW-002", Title = "Ангелы и демоны", PublicationType = PublicationType.Novel, Publisher = Publisher.AST, PublicationYear = 2000 },
        new() { Id = 5, InventoryNumber = "INV-005", CatalogCode = "FIC-BRW-003", Title = "Инферно", PublicationType = PublicationType.Novel, Publisher = Publisher.Eksmo, PublicationYear = 2013 },
        new() { Id = 6, InventoryNumber = "INV-006", CatalogCode = "FIC-STR-001", Title = "Пикник на обочине", PublicationType = PublicationType.Novel, Publisher = Publisher.AST, PublicationYear = 1972 },
        new() { Id = 7, InventoryNumber = "INV-007", CatalogCode = "FIC-TOL-001", Title = "Война и мир", PublicationType = PublicationType.Novel, Publisher = Publisher.Eksmo, PublicationYear = 1869 },
        new() { Id = 8, InventoryNumber = "INV-008", CatalogCode = "FIC-BUL-001", Title = "Мастер и Маргарита", PublicationType = PublicationType.Novel, Publisher = Publisher.AST, PublicationYear = 1967 },
        new() { Id = 9, InventoryNumber = "INV-009", CatalogCode = "SCI-001", Title = "Физика для вузов", PublicationType = PublicationType.Textbook, Publisher = Publisher.Piter, PublicationYear = 2023 },
        new() { Id = 10, InventoryNumber = "INV-010", CatalogCode = "SCI-002", Title = "Норвежский лес", PublicationType = PublicationType.Textbook, Publisher = Publisher.Prosveshchenie, PublicationYear = 2022 },
        new() { Id = 11, InventoryNumber = "INV-011", CatalogCode = "REF-001", Title = "Большой энциклопедический словарь", PublicationType = PublicationType.ReferenceBook, Publisher = Publisher.AST, PublicationYear = 2020 },
        new() { Id = 12, InventoryNumber = "INV-012", CatalogCode = "TUT-001", Title = "Программирование на C#", PublicationType = PublicationType.Tutorial, Publisher = Publisher.DMKPress, PublicationYear = 2024 }
        ];

    public static List<Reader> GetReaders() =>
    [
        new() { Id = 1, FullName = "Илон Маск", Address = "ул. Солнечная, д. 1, кв. 1", Phone = "+79160000001", RegistrationDate = new DateTime(2023, 1, 10) },
        new() { Id = 2, FullName = "Бейонсе Ноулз", Address = "пр. Лесной, д. 22, кв. 2", Phone = "+79160000002", RegistrationDate = new DateTime(2023, 2, 15) },
        new() { Id = 3, FullName = "Джон Леннон", Address = "ул. Гагарина, д. 7, кв. 3", Phone = "+79160000003", RegistrationDate = new DateTime(2023, 3, 20) },
        new() { Id = 4, FullName = "Ангела Меркель", Address = "ул. Советская, д. 10, кв. 4", Phone = "+79160000004", RegistrationDate = new DateTime(2023, 4, 5) },
        new() { Id = 5, FullName = "Леонардо ДиКаприо", Address = "пр. Мира, д. 8, кв. 5", Phone = "+79160000005", RegistrationDate = new DateTime(2023, 5, 12) },
        new() { Id = 6, FullName = "Тейлор Свифт", Address = "ул. Цветочная, д. 15, кв. 6", Phone = "+79160000006", RegistrationDate = new DateTime(2023, 6, 8) },
        new() { Id = 7, FullName = "Билл Гейтс", Address = "ул. Лесная, д. 21, кв. 7", Phone = "+79160000007", RegistrationDate = new DateTime(2023, 7, 25) },
        new() { Id = 8, FullName = "Опра Уинфри", Address = "пр. Космонавтов, д. 11, кв. 8", Phone = "+79160000008", RegistrationDate = new DateTime(2023, 8, 14) },
        new() { Id = 9, FullName = "Стив Джобс", Address = "ул. Центральная, д. 9, кв. 9", Phone = "+79160000009", RegistrationDate = new DateTime(2023, 9, 30) },
        new() { Id = 10, FullName = "Ариана Гранде", Address = "ул. Молодежная, д. 13, кв. 10", Phone = "+79160000010", RegistrationDate = new DateTime(2023, 10, 22) },
        new() { Id = 11, FullName = "Сергей Брин", Address = "пр. Строителей, д. 4, кв. 11", Phone = "+79160000011", RegistrationDate = new DateTime(2023, 11, 18) },
        new() { Id = 12, FullName = "Эллен Дедженерес", Address = "ул. Школьная, д. 6, кв. 12", Phone = "+79160000012", RegistrationDate = new DateTime(2023, 12, 5) }
    ];

    public static void LinkAuthorsAndBooks(List<Author> authors, List<Book> books)
    {
        LinkAuthor(authors[0], books[0]); // Агата Кристи 
        LinkAuthor(authors[0], books[1]); 

        LinkAuthor(authors[6], books[2]); // Дэн Браун
        LinkAuthor(authors[6], books[3]); 
        LinkAuthor(authors[6], books[4]); 

        LinkAuthor(authors[3], books[5]); // Братья Стругацкие 
        LinkAuthor(authors[1], books[6]); // Толстой
        LinkAuthor(authors[2], books[7]); // Булгаков
        LinkAuthor(authors[12], books[8]); // Петров
        LinkAuthor(authors[5], books[9]); // Мураками
        LinkAuthor(authors[9], books[11]); // Смит Дж.
        LinkAuthor(authors[4], books[10]); // Редакция 
    }

    public static List<BookLoan> GetBookLoans(List<Book> books, List<Reader> readers) =>
    [
        new() { Id = 1, Book = books[0], BookId = books[0].Id, Reader = readers[0], ReaderId = readers[0].Id, LoanDate = new DateTime(2025, 1, 10), LoanDays = 14 },
        new() { Id = 2, Book = books[1], BookId = books[1].Id, Reader = readers[1], ReaderId = readers[1].Id, LoanDate = new DateTime(2025, 1, 15), LoanDays = 21 },
        new() { Id = 3, Book = books[2], BookId = books[2].Id, Reader = readers[2], ReaderId = readers[2].Id, LoanDate = new DateTime(2025, 2, 1), LoanDays = 7 },
        new() { Id = 4, Book = books[3], BookId = books[3].Id, Reader = readers[3], ReaderId = readers[3].Id, LoanDate = new DateTime(2025, 2, 10), LoanDays = 30 },
        new() { Id = 5, Book = books[4], BookId = books[4].Id, Reader = readers[4], ReaderId = readers[4].Id, LoanDate = new DateTime(2025, 3, 5), LoanDays = 14 },
        new() { Id = 6, Book = books[5], BookId = books[5].Id, Reader = readers[5], ReaderId = readers[5].Id, LoanDate = new DateTime(2025, 3, 15), LoanDays = 21 },
        new() { Id = 7, Book = books[6], BookId = books[6].Id, Reader = readers[6], ReaderId = readers[6].Id, LoanDate = new DateTime(2025, 4, 1), LoanDays = 30 },
        new() { Id = 8, Book = books[7], BookId = books[7].Id, Reader = readers[7], ReaderId = readers[7].Id, LoanDate = new DateTime(2025, 4, 12), LoanDays = 10 },
        new() { Id = 9, Book = books[8], BookId = books[8].Id, Reader = readers[8], ReaderId = readers[8].Id, LoanDate = new DateTime(2025, 5, 3), LoanDays = 14 },
        new() { Id = 10, Book = books[9], BookId = books[9].Id, Reader = readers[9], ReaderId = readers[9].Id, LoanDate = new DateTime(2025, 5, 20), LoanDays = 21 },
        new() { Id = 11, Book = books[10], BookId = books[10].Id, Reader = readers[10], ReaderId = readers[10].Id, LoanDate = new DateTime(2025, 6, 7), LoanDays = 60 },
        new() { Id = 12, Book = books[11], BookId = books[11].Id, Reader = readers[11], ReaderId = readers[11].Id, LoanDate = new DateTime(2025, 6, 15), LoanDays = 14 },
        
        // Дополнительные выдачи для топов и популярности
        new() { Id = 13, Book = books[0], BookId = books[0].Id, Reader = readers[1], ReaderId = readers[1].Id, LoanDate = new DateTime(2025, 7, 1), LoanDays = 21 },
        new() { Id = 14, Book = books[2], BookId = books[2].Id, Reader = readers[0], ReaderId = readers[0].Id, LoanDate = new DateTime(2025, 7, 10), LoanDays = 14 },
        new() { Id = 15, Book = books[4], BookId = books[4].Id, Reader = readers[2], ReaderId = readers[2].Id, LoanDate = new DateTime(2025, 8, 5), LoanDays = 30 },
        new() { Id = 16, Book = books[9], BookId = books[9].Id, Reader = readers[2], ReaderId = readers[2].Id, LoanDate = new DateTime(2025, 6, 5), LoanDays = 10 },
        new() { Id = 17, Book = books[2], BookId = books[2].Id, Reader = readers[4], ReaderId = readers[4].Id, LoanDate = new DateTime(2025, 9, 5), LoanDays = 30 }
    ];
}
