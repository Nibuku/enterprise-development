using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

public class BookReaderMongoRepository: IRepositoryAsync<BookReader, int>
{
    private readonly IMongoCollection<BookReader> _readers;
    private int _maxId;

    public BookReaderMongoRepository(MongoDbContext context)
    {
        _readers = context.GetCollection<BookReader>("Readers");

        var last = _readers.Find(_ => true)
            .SortByDescending(x => x.Id)
            .FirstOrDefaultAsync();
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    public async Task<int> Create(BookReader reader)
    {
        reader.Id = Interlocked.Increment(ref _maxId);
        await _readers.InsertOneAsync(reader);
        return reader.Id;
    }

    public async Task<BookReader?> Read(int id)
    {
        return await _readers.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<BookReader>> ReadAll()
    {
        return await _readers.Find(_ => true).ToListAsync();
    }

    public async Task<BookReader?> Update(BookReader reader)
    {
        var update_reader = await Read(reader.Id);
        if (update_reader == null)
            return null;

        update_reader.Address = reader.Address;
        update_reader.Phone = reader.Phone;
        update_reader.FullName = reader.FullName;
        update_reader.RegistrationDate = reader.RegistrationDate;

        await _readers.ReplaceOneAsync(x => x.Id == update_reader.Id, update_reader);
        return update_reader;
    }

    public async Task<bool> Delete(int id)
    {
        var deleted_check = await _readers.DeleteOneAsync(x => x.Id == id);
        return deleted_check.DeletedCount > 0;
    }
}
