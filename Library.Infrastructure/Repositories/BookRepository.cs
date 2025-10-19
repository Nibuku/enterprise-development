using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для Book.
/// Использует данные из DataSeed.
/// </summary>
public class BookRepository: IRepositories<Book, int>
{
    private readonly List<Book> _books;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public BookRepository()
    {
        _books = DataSeed.Books;
        _maxId = _books.Count > 0 ? _books.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Создает запись о книге.
    /// Генерирует новый Id и добавляет книгу в коллекцию.
    /// </summary>
    /// <param name="book"> Объект Book. </param>
    public void Create(Book book)
    {
        book.Id = ++_maxId;
        _books.Add(book);
    }

    /// <summary>
    /// Обновляет информацию о существующей книге.
    /// </summary>
    /// <param name="book"> Обновленный объект Book </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если книга указанным Id не найдена. </exception>
    public void Update(Book book)
    {
        var update_book = Read(book.Id) ?? throw new KeyNotFoundException($"Книга с Id {book.Id} не найдена.");
        update_book.Title = book.Title;
        update_book.Authors = book.Authors;
        update_book.Publisher = book.Publisher;
        update_book.InventoryNumber = book.InventoryNumber;
        update_book.PublicationYear = book.PublicationYear;
        update_book.CatalogCode = book.CatalogCode;
        update_book.PublicationType = book.PublicationType;
    }

    /// <summary>
    /// Метод возвращает книгу по заданному Id.
    /// </summary>
    /// <returns>Объект Book. </returns>
    public Book? Read(int id)
    {
        return _books.FirstOrDefault(a => a.Id == id);
    }

    /// <summary>
    /// Метод возвращает все книги.
    /// </summary>
    /// <returns> Список всех объектов Book. </returns>
    public List<Book> ReadAll() 
    { 
        return [.. _books];
    }

    /// <summary>
    /// Удаляет книгу по Id.
    /// </summary>
    /// <param name="id"> Id книги, которую нужно удалить. </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если книга с указанным Id не найдена. </exception>
    public void Delete(int id)
    {
        var deleted_book = Read(id) ?? throw new KeyNotFoundException($"Книга с Id {id} не найдена.");
        _books.Remove(deleted_book);
    }
}
