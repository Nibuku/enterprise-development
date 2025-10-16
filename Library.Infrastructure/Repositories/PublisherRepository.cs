using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;
using System;

namespace Library.Infrastructure.Repositories;
public class PublisherRepository : IRepositories<Publisher, int>
{
    private readonly List<Publisher> _publishers;
    private int _maxId;

    public PublisherRepository()
    {
        _publishers = DataSeed.Publishers;
        _maxId = _publishers.Count > 0 ? _publishers.Max(r => r.Id) : 0;
    }

    public Publisher? Read(int id)
    {
        return _publishers.FirstOrDefault(t => t.Id == id);
    }

    public List<Publisher> ReadAll()
    {
        return _publishers;
    }

    public void Create(Publisher publisher)
    {
        publisher.Id = ++_maxId;
        _publishers.Add(publisher);
    }

    public void Update(Publisher publisher)
    {
        var update_publisher = Read(publisher.Id) ?? throw new KeyNotFoundException($"Publisher with ID {publisher.Id} not found for update.");
        update_publisher.Name = publisher.Name;
    }

    public void Delete(int id)
    {
        var deleted_publisher = Read(id) ?? throw new KeyNotFoundException($"Publisher with ID {id} not found for delete.");
        _publishers.Remove(deleted_publisher);

    }
}
