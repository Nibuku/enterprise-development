using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для Book.
/// Использует данные из DataSeed.
/// </summary>
public class BookRepository: IRepositoryAsync<Book, int>
{
    private readonly List<Book> _books;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public BookRepository()
    {
        _books = [.. DataSeed.Books];
        _maxId = _books.Count > 0 ? _books.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Создает запись о книге.
    /// Генерирует новый Id и добавляет книгу в коллекцию.
    /// </summary>
    /// <param name="book"> Объект Book. </param>
    /// <returns> Id созданной книги.</returns>
    public Task<int> Create(Book book)
    {
        book.Id = ++_maxId;
        _books.Add(book);
        return Task.FromResult(book.Id);
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
        return update_book;
    }

    /// <summary>
    /// Метод возвращает книгу по заданному Id.
    /// </summary>
    /// <returns>Объект Book.</returns>
    public Task<Book?> Read(int id)
    {
        return Task.FromResult(_books.FirstOrDefault(a => a.Id == id));
    }

    /// <summary>
    /// Метод возвращает все книги.
    /// </summary>
    /// <returns> Список всех объектов Book.</returns>
    public Task<List<Book>> ReadAll() 
    { 
        return Task.FromResult<List<Book>>([.. _books]);
    }

    /// <summary>
    /// Удаляет книгу по Id.
    /// </summary>
    /// <param name="id"> Id книги, которую нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public async Task<bool> Delete(int id)
    {
        var deleted_book =await Read(id);
        if (deleted_book == null) 
            return false;

        return _books.Remove(deleted_book);
    }
}
