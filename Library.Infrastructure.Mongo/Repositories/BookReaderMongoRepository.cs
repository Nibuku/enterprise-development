using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для BookReader для работы с MongoDb.
/// </summary>
public class BookReaderMongoRepository: IRepositoryAsync<BookReader, int>
{
    private readonly IMongoCollection<BookReader> _readers;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория. Получает коллекцию выдач из контекста MongoDB.
    /// и определяет текущий максимальный Id, для добавления новых объктов.
    /// </summary>
    /// <param name="context">Контекст MongoDB</param>
    public BookReaderMongoRepository(MongoDbContext context)
    {
        _readers = context.GetCollection<BookReader>("readers");

        var lastTask = _readers.Find(_ => true)
            .SortByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        var last = lastTask.GetAwaiter().GetResult();
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    /// <summary>
    /// Создает нового читателя.
    /// Генерирует новый Id и добавляет читателя в коллекцию.
    /// </summary>
    /// <param name="reader"> Объект BookReader. </param>
    /// <returns> Id созданного читателя.</returns>
    public async Task<int> Create(BookReader reader)
    {
        reader.Id = Interlocked.Increment(ref _maxId);
        await _readers.InsertOneAsync(reader);
        return reader.Id;
    }

    /// <summary>
    /// Метод возвращает читателя по заданному Id.
    /// </summary>
    /// <returns>Объект BookReader. </returns>
    public async Task<BookReader?> Read(int id)
    {
        return await _readers.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Метод возвращает всех читателей.
    /// </summary>
    /// <returns> Список всех объектов BookReader. </returns>
    public async Task<List<BookReader>> ReadAll()
    {
        return await _readers.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Обновляет информацию о существующем читателе.
    /// </summary>
    /// <param name="reader"> Обновленный объект BookReader </param>
    /// <returns> Обновленный объект, или null, если читатель не найден.</returns>
    public async Task<BookReader?> Update(BookReader reader)
    {
        var update_reader = await Read(reader.Id);
        if (update_reader == null)
            return null;

        update_reader.Address = reader.Address;
        update_reader.Phone = reader.Phone;
        update_reader.FullName = reader.FullName;
        update_reader.RegistrationDate = reader.RegistrationDate;

        await _readers.ReplaceOneAsync(x => x.Id == update_reader.Id, update_reader);
        return update_reader;
    }

    /// <summary>
    /// Удаляет читателя по Id.
    /// </summary>
    /// <param name="id"> Id читателя, которого нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public async Task<bool> Delete(int id)
    {
        var deleted_check = await _readers.DeleteOneAsync(x => x.Id == id);
        return deleted_check.DeletedCount > 0;
    }
}
