using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.Repositories;
public class PublicationTypeRepository : IRepositories<PublicationType, int> 
{
    private readonly List<PublicationType> _types;
    private int _maxId;

    public PublicationTypeRepository()
    {
        _types = DataSeed.PublicationTypes;
        _maxId = _types.Count > 0 ? _types.Max(r => r.Id) : 0;
    }

    public void Create(PublicationType type)
    {
        type.Id = ++_maxId;
        _types.Add(type);
    }

    public void Update(PublicationType type)
    {
        var update_type = Read(type.Id) ?? throw new KeyNotFoundException($"Type with ID {type.Id} not found for update.");
        update_type.Type = type.Type;
    }

    public PublicationType? Read(int id)
    {
        return _types.FirstOrDefault(t => t.Id == id);
    }
    public List<PublicationType> ReadAll() 
    {
        return _types; 
    }

    public void Delete(int id)
    {
        var deleted_type = Read(id) ?? throw new KeyNotFoundException($"Type with ID {id} not found for delete.");
        _types.Remove(deleted_type);

    }
}
