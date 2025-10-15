using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Tests;

namespace Library.Infrastructure.Repositories;
public class BookCheckoutRepository : IRepositories<BookCheckout, int>
{
    private readonly List<BookCheckout> _bookCheckouts;
    private int _maxId;

    public BookCheckoutRepository(DataSeed dataSeed)
    {
        _bookCheckouts = dataSeed.Checkouts;
        _maxId = _bookCheckouts.Count > 0 ? _bookCheckouts.Max(r => r.Id) : 0;
    }

    public List<BookCheckout> ReadAll()
    {
        return [.. _bookCheckouts];
    }

    public BookCheckout? Read(int id)
    {
        return _bookCheckouts.FirstOrDefault(r => r.Id == id);
    }
    public void Create(BookCheckout bookCheckout)
    {
        bookCheckout.Id = ++_maxId;
        _bookCheckouts.Add(bookCheckout);
    }

    public void Update(BookCheckout bookCheckout)
    {
        var update_check = Read(bookCheckout.Id) ?? throw new KeyNotFoundException($"BookCheckout with ID {bookCheckout.Id} not found for update.");

        update_check.Reader = bookCheckout.Reader;
        update_check.Book = bookCheckout.Book;
        update_check.LoanDate = bookCheckout.LoanDate;
        update_check.LoanDays = bookCheckout.LoanDays;
        
    }

    public void Delete(int id) {
        var deleted_check = Read(id) ?? throw new KeyNotFoundException($"BookCheckout with ID {id} not found for update."); _bookCheckouts.Remove(deleted_check);
    }

}