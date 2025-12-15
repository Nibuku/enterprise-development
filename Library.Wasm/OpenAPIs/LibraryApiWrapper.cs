namespace Library.Wasm.OpenAPIs;

/// <summary>
/// Обертка для доступа к API, предоставляющая CRUD-операции над основными сущностями и аналитические методы.
/// </summary>
public class LibraryApiWrapper(IConfiguration configuration)
{
    public readonly Client _client=new(configuration["Api:Url"], new HttpClient());

    #region Books CRUD
    public Task<ICollection<BookGetDto>> GetAllBooks() => _client.BookAllAsync();
    public Task<BookGetDto> GetBook(int id) => _client.BookGETAsync(id);
    public Task<BookGetDto> CreateBook(BookCreateDto dto) => _client.BookPOSTAsync(dto);
    public Task<BookGetDto> UpdateBook(int id, BookCreateDto dto) => _client.BookPUTAsync(id, dto);
    public Task DeleteBook(int id) => _client.BookDELETEAsync(id);
    #endregion

    #region BookReaders CRUD
    public Task<ICollection<BookReaderGetDto>> GetAllReaders() => _client.BookReaderAllAsync();
    public Task<BookReaderGetDto> GetReader(int id) => _client.BookReaderGETAsync(id);
    public Task<BookReaderGetDto> CreateReader(BookReaderCreateDto dto) => _client.BookReaderPOSTAsync(dto);
    public Task<BookReaderGetDto> UpdateReader(int id, BookReaderCreateDto dto) => _client.BookReaderPUTAsync(id, dto);
    public Task DeleteReader(int id) => _client.BookReaderDELETEAsync(id);
    #endregion

    #region PublicationTypes CRUD
    public Task<ICollection<PublicationTypeGetDto>> GetAllPublicationTypes()=> _client.PublicationTypeAllAsync();
    public Task<PublicationTypeGetDto> GetPublicationType(int id)=> _client.PublicationTypeGETAsync(id);
    public Task<PublicationTypeGetDto> CreatePublicationType(PublicationTypeCreateDto dto)=> _client.PublicationTypePOSTAsync(dto);
    public Task<PublicationTypeGetDto> UpdatePublicationType(int id, PublicationTypeCreateDto dto)=> _client.PublicationTypePUTAsync(id, dto);
    public Task DeletePublicationType(int id) => _client.PublicationTypeDELETEAsync(id);
    #endregion

    #region Publishers CRUD
    public Task<ICollection<PublisherGetDto>> GetAllPublishers()=> _client.PublisherAllAsync();
    public Task<PublisherGetDto> GetPublisher(int id) => _client.PublisherGETAsync(id);
    public Task<PublisherGetDto> CreatePublisher(PublisherCreateDto dto) => _client.PublisherPOSTAsync(dto);
    public Task<PublisherGetDto> UpdatePublisher(int id, PublisherCreateDto dto)=> _client.PublisherPUTAsync(id, dto);
    public Task DeletePublisher(int id) => _client.PublisherDELETEAsync(id);
    #endregion

    #region Checkouts CRUD
    public Task<ICollection<CheckoutGetDto>> GetAllCheckouts() => _client.BookCheckoutAllAsync();
    public Task<CheckoutGetDto> GetCheckout(int id) => _client.BookCheckoutGETAsync(id);
    public Task<CheckoutGetDto> CreateCheckout(CheckoutCreateDto dto) => _client.BookCheckoutPOSTAsync(dto);
    public Task<CheckoutGetDto> UpdateCheckout(int id, CheckoutCreateDto dto)=> _client.BookCheckoutPUTAsync(id, dto);
    public Task DeleteCheckout(int id) => _client.BookCheckoutDELETEAsync(id);
    #endregion

    #region Analytics
    public Task<ICollection<BookWithCountDto>> GetIssuedBooks()=> _client.IssuedBooksAsync();
    public Task<ICollection<BookReaderWithCountDto>> GetTopReadersByCountAsync(DateTimeOffset? start, DateTimeOffset? end) => _client.TopReadersByCountAsync(start, end);
    public Task<ICollection<BookReaderWithDaysDto>> GetTopReadersByDays() => _client.TopReadersByDaysAsync();
    public Task<ICollection<PublisherCountDto>> GetTopPublishers(DateTimeOffset? start) => _client.TopPublishersAsync(start);
    public Task<ICollection<BookWithCountDto>> GetLeastPopularBooks(DateTimeOffset? start) => _client.LeastPopularBooksAsync(start);
    #endregion
}
