using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для BookReader.
/// Использует данные из DataSeed.
/// </summary>
public class BookReaderRepository : IRepository<BookReader, int>
{
    private readonly List<BookReader> _readers;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public BookReaderRepository()
    {
        _readers = [.. DataSeed.Readers];
        _maxId = _readers.Count > 0 ? _readers.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Метод возвращает читателя по заданному Id.
    /// </summary>
    /// <returns>Объект BookReader. </returns>
    public BookReader? Read(int id)
    {
        return _readers.FirstOrDefault(a => a.Id == id);
    }

    /// <summary>
    /// Метод возвращает всех читателей.
    /// </summary>
    /// <returns> Список всех объектов BookReader. </returns>
    public List<BookReader> ReadAll()
    {
        return [.. _readers];
    }

    /// <summary>
    /// Создает нового читателя.
    /// Генерирует новый Id и добавляет читателя в коллекцию.
    /// </summary>
    /// <param name="reader"> Объект BookReader. </param>
    /// <returns> Id созданного читателя.</returns>
    public int Create(BookReader reader)
    {
        reader.Id = ++_maxId;
        _readers.Add(reader);
        return reader.Id;
    }

    /// <summary>
    /// Обновляет информацию о существующем читателе.
    /// </summary>
    /// <param name="reader"> Обновленный объект BookReader </param>
    /// <returns> Обновленный объект, или null, если читатель не найден.</returns>
    public BookReader? Update(BookReader reader)
    {
        var update_reader = Read(reader.Id);
        if (update_reader== null)
            return null;

        update_reader.Address = reader.Address;
        update_reader.Phone = reader.Phone;
        update_reader.FullName = reader.FullName;
        update_reader.RegistrationDate = reader.RegistrationDate;
        return update_reader;
    }

    /// <summary>
    /// Удаляет читателя по Id.
    /// </summary>
    /// <param name="id"> Id читателя, которого нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public bool Delete(int id)
    {
        var deleted_reader = Read(id);
        if (deleted_reader == null)
            return false;

        return _readers.Remove(deleted_reader); 
    }
}
