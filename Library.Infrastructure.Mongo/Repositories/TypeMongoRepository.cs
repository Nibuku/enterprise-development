using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для объектов PublicationType для работы с MongoDb.
/// </summary>
public class TypeMongoRepository: IRepositoryAsync<PublicationType, int>
{
    private readonly IMongoCollection<PublicationType> _types;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория. Получает коллекцию выдач из контекста MongoDB.
    /// и определяет текущий максимальный Id, для добавления новых объктов.
    /// </summary>
    /// <param name="context">Контекст MongoDB</param>
    public TypeMongoRepository(MongoDbContext context)
    {
        _types = context.GetCollection<PublicationType>("types");

        var last = _types.Find(_ => true)
            .SortByDescending(x => x.Id)
            .FirstOrDefaultAsync();
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    /// <summary>
    /// Вносит новый тип публикации.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="type"> Объект PublicationType. </param>
    /// <returns> Id созданного типа публикации.</returns>
    public async Task<int> Create(PublicationType type)
    {
        type.Id = Interlocked.Increment(ref _maxId);
        await _types.InsertOneAsync(type);
        return type.Id;
    }


    /// <summary>
    /// Метод возвращает тип публикации по заданному Id.
    /// </summary>
    /// <returns>Объект PublicationType.</returns>
    public async Task<PublicationType?> Read(int id)
    {
        return await _types.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Метод возвращает список всех типой публикаций.
    /// </summary>
    /// <returns> Список всех объектов PublicationType.</returns>
    public async Task<List<PublicationType>> ReadAll()
    {
        return await _types.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Обновляет существующий тип публикации.
    /// </summary>
    /// <param name="type"> Обновленный объект PublicationType </param>
    /// <returns> Обновленный объект, или null, если тип публикации не найден.</returns>
    public async Task<PublicationType?> Update(PublicationType type)
    {
        var update_type = await Read(type.Id);
        if (update_type == null)
            return null;

        update_type.Type = type.Type;

        await _types.ReplaceOneAsync(x => x.Id == update_type.Id, update_type);
        return update_type;
    }

    /// <summary>
    /// Удаляет тип публикации по Id.
    /// </summary>
    /// <param name="id"> Id типа, который нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public async Task<bool> Delete(int id)
    {
        var deleted_type = await _types.DeleteOneAsync(x => x.Id == id);
        return deleted_type.DeletedCount > 0;
    }
}
