using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для Book.
/// Использует данные из DataSeed.
/// </summary>
public class BookRepository: IRepository<Book, int>
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
    public int Create(Book book)
    {
        book.Id = ++_maxId;
        _books.Add(book);
        return book.Id;
    }

    /// <summary>
    /// Обновляет информацию о существующей книге.
    /// </summary>
    /// <param name="book"> Обновленный объект Book </param>
    /// <returns> Обновлённая книга или null, если не найдена. </returns>
    public Book? Update(Book book)
    {
        var update_book = Read(book.Id);
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
    public Book? Read(int id)
    {
        return _books.FirstOrDefault(a => a.Id == id);
    }

    /// <summary>
    /// Метод возвращает все книги.
    /// </summary>
    /// <returns> Список всех объектов Book.</returns>
    public List<Book> ReadAll() 
    { 
        return [.. _books];
    }

    /// <summary>
    /// Удаляет книгу по Id.
    /// </summary>
    /// <param name="id"> Id книги, которую нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public bool Delete(int id)
    {
        var deleted_book = Read(id);
        if (deleted_book == null) 
            return false;

        return _books.Remove(deleted_book);
    }
}
