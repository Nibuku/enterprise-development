using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

public class PublisherMongoRepository: IRepositoryAsync<Publisher, int>
{
    private readonly IMongoCollection<Publisher> _publishers;
    private int _maxId;

    public PublisherMongoRepository(MongoDbContext context)
    {
        _publishers = context.GetCollection<Publisher>("Publishers");

        var last = _publishers.Find(_ => true)
            .SortByDescending(x => x.Id)
            .FirstOrDefaultAsync();
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    public async Task<int> Create(Publisher publisher)
    {
        publisher.Id = Interlocked.Increment(ref _maxId);
        await _publishers.InsertOneAsync(publisher);
        return publisher.Id;
    }

    public async Task<Publisher?> Read(int id)
    {
        return await _publishers.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Publisher>> ReadAll()
    {
        return await _publishers.Find(_ => true).ToListAsync();
    }

    public async Task<Publisher?> Update(Publisher publisher)
    {
        var update_publisher = await Read(publisher.Id);
        if (update_publisher == null)
            return null;

        update_publisher.Name = publisher.Name;

        await _publishers.ReplaceOneAsync(x => x.Id == update_publisher.Id, update_publisher);
        return update_publisher;
    }

    public async Task<bool> Delete(int id)
    {
        var deleted_book = await _publishers.DeleteOneAsync(x => x.Id == id);
        return deleted_book.DeletedCount > 0;
    }
}
