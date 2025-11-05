using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для объектов BookCheckout для работы с MongoDb.
/// </summary>
public class BookCheckoutMongoRepository: IRepositoryAsync<BookCheckout, int>
{
    private readonly IMongoCollection<BookCheckout> _checks;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория. Получает коллекцию выдач из контекста MongoDB.
    /// и определяет текущий максимальный Id, для добавления новых объктов.
    /// </summary>
    /// <param name="context">Контекст MongoDB</param>
    public BookCheckoutMongoRepository(MongoDbContext context)
    {
        _checks = context.GetCollection<BookCheckout>("checkouts");
        var lastTask = _checks.Find(_ => true)         
                      .SortByDescending(x => x.Id)  
                      .FirstOrDefaultAsync();    

        var last = lastTask.GetAwaiter().GetResult(); 
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    /// <summary>
    /// Создает запись о выдаче.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="bookCheckout"> Объект BookCheckout. </param>
    /// <returns> Id созданной записи о выдаче.</returns>
    public async Task<int> Create(BookCheckout bookCheckout)
    {
        bookCheckout.Id = Interlocked.Increment(ref _maxId);
        await _checks.InsertOneAsync(bookCheckout);
        return bookCheckout.Id;
    }

    /// <summary>
    /// Метод возвращает запись о выдаче по заданному Id.
    /// </summary>
    /// <returns>Объект BookCheckout. </returns>
    public async Task<BookCheckout?> Read(int id)
    {
        return await _checks.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Метод возвращает все записи о выдаче книг.
    /// </summary>
    /// <returns> Список всех объектов BookCheckout. </returns>
    public async Task<List<BookCheckout>> ReadAll()
    {
        return await _checks.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Обновляет существующую запись о выдаче.
    /// </summary>
    /// <param name="bookCheckout"> Обновленный объект BookCheckout </param>
    /// <returns> Обновленный объект, или null, если запись о выдаче не найдена.</returns>
    public async Task<BookCheckout?> Update(BookCheckout bookCheckout)
    {
        var update_check = await Read(bookCheckout.Id);
        if (update_check == null)
            return null;

        update_check.Reader = bookCheckout.Reader;
        update_check.Book = bookCheckout.Book;
        update_check.LoanDate = bookCheckout.LoanDate;
        update_check.LoanDays = bookCheckout.LoanDays;

        await _checks.ReplaceOneAsync(x => x.Id == update_check.Id, update_check);
        return update_check;
    }

    /// <summary>
    /// Удаляет запись о выдаче по Id.
    /// </summary>
    /// <param name="id"> Id записи, которую нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public async Task<bool> Delete(int id)
    {
        var deleted_check = await _checks.DeleteOneAsync(x => x.Id == id);
        return deleted_check.DeletedCount > 0;
    }
}
