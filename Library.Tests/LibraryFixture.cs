using Library.Infrastructure.InMemory.Repositories;

namespace Library.Tests;

/// <summary>
/// Фикстура для тестов.
/// Инициализируется с помощью репозиториев.
/// </summary>
public class LibraryFixture
{
    /// <summary>
    /// Репозиторий для книг.
    /// </summary>
    public BookRepository BookRepository { get; }

    /// <summary>
    /// Репозиторий для читателей.
    /// </summary>
    public BookReaderRepository ReaderRepository { get; }

    /// <summary>
    /// Репозиторий для выдач.
    /// </summary>
    public BookCheckoutRepository CheckoutRepository { get; }

    /// <summary>
    /// Репозиторий для издательств.
    /// </summary>
    public PublisherRepository PublisherRepository { get; }

    /// <summary>
    /// Репозиторий для типов публикаций.
    /// </summary>
    public PublicationTypeRepository PublicationTypeRepository { get; }

    /// <summary>
    /// Создает экземпляр фикстуры с репозиториями, заполненными тестовыми данными.
    /// </summary>
    public LibraryFixture()
    {
        BookRepository = new BookRepository();
        ReaderRepository = new BookReaderRepository();
        CheckoutRepository = new BookCheckoutRepository();
        PublisherRepository = new PublisherRepository();    
        PublicationTypeRepository = new PublicationTypeRepository();
    }
}
