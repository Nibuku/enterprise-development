using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для Publisher для работы с MongoDb.
/// </summary>
public class PublisherMongoRepository: IRepositoryAsync<Publisher, int>
{
    private readonly IMongoCollection<Publisher> _publishers;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория. Получает коллекцию выдач из контекста MongoDB.
    /// и определяет текущий максимальный Id, для добавления новых объктов.
    /// </summary>
    /// <param name="context">Контекст MongoDB</param>
    public PublisherMongoRepository(MongoDbContext context)
    {
        _publishers = context.GetCollection<Publisher>("publishers");

        var lastTask = _publishers.Find(_ => true)
            .SortByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        var last = lastTask.GetAwaiter().GetResult();
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    /// <summary>
    /// Вносит новое издательство.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="publisher"> Объект Publisher. </param>
    /// <returns> Id созданного издательства.</returns>
    public async Task<int> Create(Publisher publisher)
    {
        publisher.Id = Interlocked.Increment(ref _maxId);
        await _publishers.InsertOneAsync(publisher);
        return publisher.Id;
    }

    /// <summary>
    /// Метод возвращает издательство по заданному Id.
    /// </summary>
    /// <returns>Объект Publisher. </returns>
    public async Task<Publisher?> Read(int id)
    {
        return await _publishers.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Метод возвращает все записи о всех издательствах.
    /// </summary>
    /// <returns> Список всех объектов Publisher. </returns>
    public async Task<List<Publisher>> ReadAll()
    {
        return await _publishers.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Обновляет информацию о существующем издательстве.
    /// </summary>
    /// <param name="publisher"> Обновленный объект Publisher </param>
    /// <returns> Обновлённое издательство или null, если не найдена. </returns>
    public async Task<Publisher?> Update(Publisher publisher)
    {
        var update_publisher = await Read(publisher.Id);
        if (update_publisher == null)
            return null;

        update_publisher.Name = publisher.Name;

        await _publishers.ReplaceOneAsync(x => x.Id == update_publisher.Id, update_publisher);
        return update_publisher;
    }

    /// <summary>
    /// Удаляет издательство по Id.
    /// </summary>
    /// <param name="id"> Id издательства, которое нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public async Task<bool> Delete(int id)
    {
        var deleted_book = await _publishers.DeleteOneAsync(x => x.Id == id);
        return deleted_book.DeletedCount > 0;
    }
}
