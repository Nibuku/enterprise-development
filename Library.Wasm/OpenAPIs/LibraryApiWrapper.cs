namespace Library.Wasm.OpenApis;

/// <summary>
/// Обертка для доступа к API, предоставляющая CRUD-операции над основными сущностями и аналитические методы.
/// </summary>
public class LibraryApiWrapper
{
    public readonly Client _client;

    public LibraryApiWrapper(HttpClient httpClient)
    {
        var baseUrl = httpClient.BaseAddress?.ToString() ?? throw new KeyNotFoundException("BaseAddress не задан");
        _client = new Client(baseUrl, httpClient);
    }
    #region Books CRUD
    public Task<ICollection<BookGetDto>> GetAllBooks(CancellationToken ct = default) => _client.BookAllAsync(ct);
    public Task<BookGetDto> GetBook(int id, CancellationToken ct = default) => _client.BookGETAsync(id, ct);
    public Task<BookGetDto> CreateBook(BookCreateDto dto, CancellationToken ct = default) => _client.BookPOSTAsync( dto, ct);
    public Task<BookGetDto> UpdateBook(int id, BookCreateDto dto, CancellationToken ct = default) => _client.BookPUTAsync(id, dto, ct);
    public Task DeleteBook(int id, CancellationToken ct = default) => _client.BookDELETEAsync(id, ct);
    #endregion

    #region BookReaders CRUD
    public Task<ICollection<BookReaderGetDto>> GetAllReaders(CancellationToken ct = default) => _client.BookReaderAllAsync(ct);
    public Task<BookReaderGetDto> GetReader(int id, CancellationToken ct = default) => _client.BookReaderGETAsync(id, ct);
    public Task<BookReaderGetDto> CreateReader(BookReaderCreateDto dto, CancellationToken ct = default) => _client.BookReaderPOSTAsync(dto, ct);
    public Task<BookReaderGetDto> UpdateReader(int id, BookReaderCreateDto dto, CancellationToken ct = default) => _client.BookReaderPUTAsync(id, dto, ct);
    public Task DeleteReader(int id, CancellationToken ct = default) => _client.BookReaderDELETEAsync(id, ct);
    #endregion

    #region PublicationTypes CRUD
    public Task<ICollection<PublicationTypeGetDto>> GetAllPublicationTypes(CancellationToken ct = default) => _client.PublicationTypeAllAsync(ct);
    public Task<PublicationTypeGetDto> GetPublicationType(int id, CancellationToken ct = default) => _client.PublicationTypeGETAsync(id, ct);
    public Task<PublicationTypeGetDto> CreatePublicationType(PublicationTypeCreateDto dto, CancellationToken ct = default) => _client.PublicationTypePOSTAsync(dto, ct);
    public Task<PublicationTypeGetDto> UpdatePublicationType(int id, PublicationTypeCreateDto dto, CancellationToken ct = default) => _client.PublicationTypePUTAsync(id, dto, ct);
    public Task DeletePublicationType(int id, CancellationToken ct = default) => _client.PublicationTypeDELETEAsync(id, ct);
    #endregion

    #region Publishers CRUD
    public Task<ICollection<PublisherGetDto>> GetAllPublishers(CancellationToken ct = default) => _client.PublisherAllAsync(ct);
    public Task<PublisherGetDto> GetPublisher(int id, CancellationToken ct = default) => _client.PublisherGETAsync(id, ct);
    public Task<PublisherGetDto> CreatePublisher(PublisherCreateDto dto, CancellationToken ct = default) => _client.PublisherPOSTAsync(dto, ct);
    public Task<PublisherGetDto> UpdatePublisher(int id, PublisherCreateDto dto, CancellationToken ct = default) => _client.PublisherPUTAsync(id, dto, ct);
    public Task DeletePublisher(int id, CancellationToken ct = default) => _client.PublisherDELETEAsync(id, ct);
    #endregion

    #region Checkouts CRUD
    public Task<ICollection<CheckoutGetDto>> GetAllCheckouts(CancellationToken ct = default) => _client.BookCheckoutAllAsync(ct);
    public Task<CheckoutGetDto> GetCheckout(int id, CancellationToken ct = default) => _client.BookCheckoutGETAsync(id, ct);
    public Task<CheckoutGetDto> CreateCheckout(CheckoutCreateDto dto, CancellationToken ct = default) => _client.BookCheckoutPOSTAsync(dto, ct);
    public Task<CheckoutGetDto> UpdateCheckout(int id, CheckoutCreateDto dto, CancellationToken ct = default) => _client.BookCheckoutPUTAsync(id, dto, ct);
    public Task DeleteCheckout(int id, CancellationToken ct = default) => _client.BookCheckoutDELETEAsync(id, ct);
    #endregion

    #region Analytics
    public Task<ICollection<BookWithCountDto>> GetIssuedBooks(CancellationToken ct = default) => _client.IssuedBooksAsync(ct);
    public Task<ICollection<BookReaderWithCountDto>> GetTopReadersByCountAsync(DateTimeOffset? start, DateTimeOffset? end, CancellationToken ct = default) => _client.TopReadersByCountAsync(start, end, ct);
    public Task<ICollection<BookReaderWithDaysDto>> GetTopReadersByDays(CancellationToken ct = default) => _client.TopReadersByDaysAsync(ct);
    public Task<ICollection<PublisherCountDto>> GetTopPublishers(DateTimeOffset? start, CancellationToken ct = default) => _client.TopPublishersAsync(start, ct);
    public Task<ICollection<BookWithCountDto>> GetLeastPopularBooks(DateTimeOffset? start, CancellationToken ct = default) => _client.LeastPopularBooksAsync(start, ct);
    #endregion
}
