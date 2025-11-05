using Library.Domain.Interfaces;
using Library.Domain.Models;
using MongoDB.Driver;

namespace Library.Infrastructure.Mongo.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для объектов Book для работы с MongoDb.
/// </summary>
public class BookMongoRepository: IRepositoryAsync<Book, int>
{
    private readonly IMongoCollection<Book> _books;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория. Получает коллекцию выдач из контекста MongoDB.
    /// и определяет текущий максимальный Id, для добавления новых объктов.
    /// </summary>
    /// <param name="context">Контекст MongoDB</param>
    public BookMongoRepository(MongoDbContext context)
    {
        _books = context.GetCollection<Book>("books");

        var lastTask = _books.Find(_=> true)
            .SortByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        var last = lastTask.GetAwaiter().GetResult();
        if (last != null)
            _maxId = last.Id;
        else
            _maxId = 0;
    }

    /// <summary>
    /// Создает запись о книге.
    /// Генерирует новый Id и добавляет книгу в коллекцию.
    /// </summary>
    /// <param name="book"> Объект Book. </param>
    /// <returns> Id созданной книги.</returns>
    public async Task<int> Create(Book book)
    {
        book.Id = Interlocked.Increment(ref _maxId);
        await _books.InsertOneAsync(book);
        return book.Id;
    }

    /// <summary>
    /// Метод возвращает книгу по заданному Id.
    /// </summary>
    /// <returns>Объект Book.</returns>
    public async Task<Book?> Read(int id)
    {
        return await _books.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Метод возвращает все книги.
    /// </summary>
    /// <returns> Список всех объектов Book.</returns>
    public async Task<List<Book>> ReadAll()
    {
        return await _books.Find(_=>true).ToListAsync();
    }

    /// <summary>
    /// Обновляет информацию о существующей книге.
    /// </summary>
    /// <param name="book"> Обновленный объект Book </param>
    /// <returns> Обновлённая книга или null, если не найдена. </returns>
    public async Task<Book?> Update(Book book)
    {
        var update_book =await Read(book.Id);
        if (update_book == null)
            return null;

        update_book.Title = book.Title;
        update_book.Authors = book.Authors;
        update_book.Publisher = book.Publisher;
        update_book.InventoryNumber = book.InventoryNumber;
        update_book.PublicationYear = book.PublicationYear;
        update_book.CatalogCode = book.CatalogCode;
        update_book.PublicationType = book.PublicationType;
       
        await _books.ReplaceOneAsync(x => x.Id == update_book.Id, update_book);
        return update_book;
    }

    /// <summary>
    /// Удаляет книгу по Id.
    /// </summary>
    /// <param name="id"> Id книги, которую нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public async Task<bool> Delete(int id)
    {
        var deleted_book= await _books.DeleteOneAsync(x => x.Id == id);
        return deleted_book.DeletedCount > 0;
    }
}
