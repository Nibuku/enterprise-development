using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для BookReader.
/// Использует данные из DataSeed.
/// </summary>
public class BookReaderRepository : IRepositories<BookReader, int>
{
    private readonly List<BookReader> _readers;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public BookReaderRepository()
    {
        _readers = DataSeed.Readers;
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
    public void Create(BookReader reader)
    {
        reader.Id = ++_maxId;
        _readers.Add(reader);
    }

    /// <summary>
    /// Обновляет информацию о существующем читателе.
    /// </summary>
    /// <param name="reader"> Обновленный объект BookReader </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если читатель указанным Id не найден. </exception>
    public void Update(BookReader reader)
    {
        var update_reader = Read(reader.Id) ?? throw new KeyNotFoundException($"Читатель с Id {reader.Id}не найден.");
        update_reader.Address = reader.Address;
        update_reader.Phone = reader.Phone;
        update_reader.FullName = reader.FullName;
        update_reader.RegistrationDate = reader.RegistrationDate;
    }

    /// <summary>
    /// Удаляет читателя по Id.
    /// </summary>
    /// <param name="id"> Id читателя, которого нужно удалить. </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если читатель с указанным Id не найден. </exception>
    public void Delete(int id)
    {
        var deleted_reader = Read(id) ?? throw new KeyNotFoundException($"Читатель с Id {id} не найден.");
        _readers.Remove(deleted_reader);
    }
}
