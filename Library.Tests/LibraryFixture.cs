using Library.Infrastructure.Repositories;

namespace Library.Tests;
public class LibraryFixture
{
    public BookRepository BookRepository { get; }
    public BookReaderRepository ReaderRepository { get; }
    public BookCheckoutRepository CheckoutRepository { get; }
    public PublisherRepository PublisherRepository { get; }
    public PublicationTypeRepository PublicationTypeRepository { get; }

    public LibraryFixture()
    {
        BookRepository = new BookRepository();
        ReaderRepository = new BookReaderRepository();
        CheckoutRepository = new BookCheckoutRepository();
        PublisherRepository = new PublisherRepository();    
        PublicationTypeRepository = new PublicationTypeRepository();
    }

}
