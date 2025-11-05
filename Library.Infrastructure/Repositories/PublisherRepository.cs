using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для Publisher.
/// Использует данные из DataSeed.
/// </summary>
public class PublisherRepository : IRepositoryAsync<Publisher, int>
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
    public Task<Publisher?> Read(int id)
    {
        return Task.FromResult(_publishers.FirstOrDefault(t => t.Id == id));
    }

    /// <summary>
    /// Метод возвращает все записи о всех издательствах.
    /// </summary>
    /// <returns> Список всех объектов Publisher. </returns>
    public Task<List<Publisher>> ReadAll()
    {
        return Task.FromResult<List<Publisher>>([.. _publishers]);
    }

    /// <summary>
    /// Вносит новое издательство.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="publisher"> Объект Publisher. </param>
    /// <returns> Id созданного издательства.</returns>
    public Task<int> Create(Publisher publisher)
    {
        publisher.Id = ++_maxId;
        _publishers.Add(publisher);
        return Task.FromResult(publisher.Id);
    }

    /// <summary>
    /// Обновляет информацию о существующем издательстве.
    /// </summary>
    /// <param name="publisher"> Обновленный объект Publisher </param>
    /// <returns> Обновлённое издательство или null, если не найдена. </returns>
    public async Task<Publisher?> Update(Publisher publisher)
    {
        var update_publisher =await Read(publisher.Id);
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
    public async Task<bool> Delete(int id)
    {
        var deleted_publisher =await Read(id);
        if (deleted_publisher == null) 
            return false;

        return _publishers.Remove(deleted_publisher); ;
    }
}
