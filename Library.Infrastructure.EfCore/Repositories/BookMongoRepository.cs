using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

public class BookMongoRepository: IRepositoryAsync<Book, int>
{
    private readonly IMongoCollection<Book> _books;
    private int _maxId;

    public BookMongoRepository(MongoDbContext context)
    {
        _books = context.GetCollection<Book>("Books");

        var last = _books.Find(_=> true)
            .SortByDescending(x => x.Id)
            .FirstOrDefaultAsync();
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    public async Task<int> Create(Book book)
    {
        book.Id = Interlocked.Increment(ref _maxId);
        await _books.InsertOneAsync(book);
        return book.Id;
    }

    public async Task<Book?> Read(int id)
    {
        return await _books.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Book>> ReadAll()
    {
        return await _books.Find(_=>true).ToListAsync();
    }

    public async Task<Book?> Update(Book book)
    {
        var update_book =await Read(book.Id);
        if (update_book == null)
            return null;

        update_book.Title = book.Title;
        update_book.Authors = book.Authors;
        update_book.Publisher = book.Publisher;
        update_book.InventoryNumber = book.InventoryNumber;
        update_book.PublicationYear = book.PublicationYear;
        update_book.CatalogCode = book.CatalogCode;
        update_book.PublicationType = book.PublicationType;
       
        await _books.ReplaceOneAsync(x => x.Id == update_book.Id, update_book);
        return update_book;
    }

    public async Task<bool> Delete(int id)
    {
        var deleted_book= await _books.DeleteOneAsync(x => x.Id == id);
        return deleted_book.DeletedCount > 0;
    }
}
