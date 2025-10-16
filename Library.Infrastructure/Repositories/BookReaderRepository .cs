using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.Repositories;
public class BookReaderRepository : IRepositories<BookReader, int>
{
    private readonly List<BookReader> _readers;
    private int _maxId;

    public BookReaderRepository()
    {
        _readers = DataSeed.Readers;
        _maxId = _readers.Count > 0 ? _readers.Max(r => r.Id) : 0;
    }

    public BookReader? Read(int id)
    {
        return _readers.FirstOrDefault(a => a.Id == id);
    }

    public List<BookReader> ReadAll()
    {
        return [.. _readers];
    }

    public void Create(BookReader reader)
    {
        reader.Id = ++_maxId;
        _readers.Add(reader);
    }

    public void Update(BookReader reader)
    {
        var update_reader = Read(reader.Id) ?? throw new KeyNotFoundException($"BookReader with ID {reader.Id} not found for update.");
        update_reader.Address = reader.Address;
        update_reader.Phone = reader.Phone;
        update_reader.FullName = reader.FullName;
        update_reader.RegistrationDate = reader.RegistrationDate;
        
    }

    public void Delete(int id)
    {
        var deleted_reader = Read(id) ?? throw new KeyNotFoundException($"BookReader with ID {id} not found for update.");
        _readers.Remove(deleted_reader);
    }
}
