using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

public class BookCheckoutMongoRepository: IRepositoryAsync<BookCheckout, int>
{
    private readonly IMongoCollection<BookCheckout> _checks;
    private int _maxId;

    public BookCheckoutMongoRepository(MongoDbContext context)
    {
        _checks = context.GetCollection<BookCheckout>("Checkouts");

        var last = _checks.Find(_ => true)
            .SortByDescending(x => x.Id)
            .FirstOrDefaultAsync();
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    public async Task<int> Create(BookCheckout bookCheckout)
    {
        bookCheckout.Id = Interlocked.Increment(ref _maxId);
        await _checks.InsertOneAsync(bookCheckout);
        return bookCheckout.Id;
    }

    public async Task<BookCheckout?> Read(int id)
    {
        return await _checks.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<BookCheckout>> ReadAll()
    {
        return await _checks.Find(_ => true).ToListAsync();
    }

    public async Task<BookCheckout?> Update(BookCheckout bookCheckout)
    {
        var update_check = await Read(bookCheckout.Id);
        if (update_check == null)
            return null;

        update_check.Reader = bookCheckout.Reader;
        update_check.Book = bookCheckout.Book;
        update_check.LoanDate = bookCheckout.LoanDate;
        update_check.LoanDays = bookCheckout.LoanDays;

        await _checks.ReplaceOneAsync(x => x.Id == update_check.Id, update_check);
        return update_check;
    }

    public async Task<bool> Delete(int id)
    {
        var deleted_check = await _checks.DeleteOneAsync(x => x.Id == id);
        return deleted_check.DeletedCount > 0;
    }
}
