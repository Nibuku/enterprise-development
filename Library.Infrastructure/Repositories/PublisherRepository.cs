using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для Publisher.
/// Использует данные из DataSeed.
/// </summary>
public class PublisherRepository : IRepositories<Publisher, int>
{
    private readonly List<Publisher> _publishers;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public PublisherRepository()
    {
        _publishers = DataSeed.Publishers;
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
        return _publishers;
    }

    /// <summary>
    /// Вносит новое издательство.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="publisher"> Объект Publisher. </param>
    public void Create(Publisher publisher)
    {
        publisher.Id = ++_maxId;
        _publishers.Add(publisher);
    }

    /// <summary>
    /// Обновляет информацию о существующем издательстве.
    /// </summary>
    /// <param name="publisher"> Обновленный объект Publisher </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если издательство с указанным Id не найдено. </exception>
    public void Update(Publisher publisher)
    {
        var update_publisher = Read(publisher.Id) ?? throw new KeyNotFoundException($"Издательство с Id {publisher.Id} не найдено.");
        update_publisher.Name = publisher.Name;
    }

    /// <summary>
    /// Удаляет издательство по Id.
    /// </summary>
    /// <param name="id"> Id издательства, которое нужно удалить. </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если издательство с указанным Id не найдено. </exception>
    public void Delete(int id)
    {
        var deleted_publisher = Read(id) ?? throw new KeyNotFoundException($"Издательство с Id {id} не найдено.");
        _publishers.Remove(deleted_publisher);
    }
}
