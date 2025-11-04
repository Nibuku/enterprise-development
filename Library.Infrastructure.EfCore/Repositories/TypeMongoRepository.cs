using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

public class TypeMongoRepository: IRepositoryAsync<PublicationType, int>
{
    private readonly IMongoCollection<PublicationType> _types;
    private int _maxId;

    public TypeMongoRepository(MongoDbContext context)
    {
        _types = context.GetCollection<PublicationType>("Types");

        var last = _types.Find(_ => true)
            .SortByDescending(x => x.Id)
            .FirstOrDefaultAsync();
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    public async Task<int> Create(PublicationType type)
    {
        type.Id = Interlocked.Increment(ref _maxId);
        await _types.InsertOneAsync(type);
        return type.Id;
    }

    public async Task<PublicationType?> Read(int id)
    {
        return await _types.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<PublicationType>> ReadAll()
    {
        return await _types.Find(_ => true).ToListAsync();
    }

    public async Task<PublicationType?> Update(PublicationType type)
    {
        var update_type = await Read(type.Id);
        if (update_type == null)
            return null;

        update_type.Type = type.Type;

        await _types.ReplaceOneAsync(x => x.Id == update_type.Id, update_type);
        return update_type;
    }

    public async Task<bool> Delete(int id)
    {
        var deleted_type = await _types.DeleteOneAsync(x => x.Id == id);
        return deleted_type.DeletedCount > 0;
    }
}
