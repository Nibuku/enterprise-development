using MongoDB.Driver;

namespace Library.Infrastructure.Mongo;

/// <summary>
/// Класс-контекст MongoDB — вспомогательный класс, для упрощения работы с базой данных.
/// <summary>
public class MongoDbContext(IMongoDatabase database)
{
    private readonly IMongoDatabase _database = database;

    /// <summary>
    /// Возвращает коллекцию нужного типа.
    /// </summary>
    /// <typeparam name="T">Тип объектов</typeparam>
    /// <param name="name">Название коллекции</param>
    /// <returns>Коллекция MongoDB</returns>
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}
