using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.Repositories;

public class BookRepository: IRepositories<Book, int>
{
    private readonly List<Book> _books;
    private int _maxId;


    public BookRepository()
    {
            _books = DataSeed.Books;
            _maxId = _books.Count > 0 ? _books.Max(r => r.Id) : 0;
    }
    public void Create(Book book)
    {
        book.Id = ++_maxId;
        _books.Add(book);
    }

    public void Update(Book book)
    {
        var update_book = Read(book.Id) ?? throw new KeyNotFoundException($"Book with ID {book.Id} not found for update.");
        update_book.Title = book.Title;
        update_book.Authors = book.Authors;
        update_book.Publisher = book.Publisher;
        update_book.InventoryNumber = book.InventoryNumber;
        update_book.PublicationYear = book.PublicationYear;
        update_book.CatalogCode = book.CatalogCode;
        update_book.PublicationType = book.PublicationType;
    }
    public Book? Read(int id)
    {
        return _books.FirstOrDefault(a => a.Id == id);
    }

    public List<Book> ReadAll() 
    { 
        return [.. _books];
    }

    public void Delete(int id)
    {
        var deleted_book = Read(id) ?? throw new KeyNotFoundException($"Book with ID {id} not found for update.");
        _books.Remove(deleted_book);
        
    }

}
