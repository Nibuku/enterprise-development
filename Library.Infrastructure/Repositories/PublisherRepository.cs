using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для Publisher.
/// Использует данные из DataSeed.
/// </summary>
public class PublisherRepository : IRepository<Publisher, int>
{
    private readonly List<Publisher> _publishers;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public PublisherRepository()
    {
        _publishers = [.. DataSeed.Publishers];
        _maxId = _publishers.Count > 0 ? _publishers.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Метод возвращает издательство по заданному Id.
    /// </summary>
    /// <returns>Объект Publisher. </returns>
    public Publisher? Read(int id)
    {
        return _publishers.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Метод возвращает все записи о всех издательствах.
    /// </summary>
    /// <returns> Список всех объектов Publisher. </returns>
    public List<Publisher> ReadAll()
    {
        return [.. _publishers];
    }

    /// <summary>
    /// Вносит новое издательство.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="publisher"> Объект Publisher. </param>
    /// <returns> Id созданной книги.</returns>
    public int Create(Publisher publisher)
    {
        publisher.Id = ++_maxId;
        _publishers.Add(publisher);
        return publisher.Id;
    }

    /// <summary>
    /// Обновляет информацию о существующем издательстве.
    /// </summary>
    /// <param name="publisher"> Обновленный объект Publisher </param>
    /// <returns> Обновлённое издательство или null, если не найдена. </returns>
    public Publisher? Update(Publisher publisher)
    {
        var update_publisher = Read(publisher.Id);
        if (update_publisher == null) 
            return null;

        update_publisher.Name = publisher.Name;
        return update_publisher;
    }

    /// <summary>
    /// Удаляет издательство по Id.
    /// </summary>
    /// <param name="id"> Id издательства, которое нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public bool Delete(int id)
    {
        var deleted_publisher = Read(id);
        if (deleted_publisher == null) 
            return false;

        return _publishers.Remove(deleted_publisher); ;
    }
}
