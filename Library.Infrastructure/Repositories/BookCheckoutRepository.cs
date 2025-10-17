using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для BookCheckout.
/// Использует данные из DataSeed.
/// </summary>
public class BookCheckoutRepository : IRepositories<BookCheckout, int>
{
    private readonly List<BookCheckout> _bookCheckouts;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public BookCheckoutRepository()
    {
        _bookCheckouts = DataSeed.Checkouts;
        _maxId = _bookCheckouts.Count > 0 ? _bookCheckouts.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Метод возвращает все записи о выдаче книг.
    /// </summary>
    /// <returns> Список всех объектов BookCheckout. </returns>
    public List<BookCheckout> ReadAll()
    {
        return [.. _bookCheckouts];
    }

    /// <summary>
    /// Метод возвращает запись о выдаче по заданному Id.
    /// </summary>
    /// <returns>Объект BookCheckout. </returns>
    public BookCheckout? Read(int id)
    {
        return _bookCheckouts.FirstOrDefault(r => r.Id == id);
    }

    /// <summary>
    /// Создает запись о выдаче.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="bookCheckout"> Объект BookCheckout. </param>
    public void Create(BookCheckout bookCheckout)
    {
        bookCheckout.Id = ++_maxId;
        _bookCheckouts.Add(bookCheckout);
    }

    /// <summary>
    /// Обновляет существующую запись о выдаче.
    /// </summary>
    /// <param name="bookCheckout"> Обновленный объект BookCheckout </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если запись с указанным Id не найдена. </exception>
    public void Update(BookCheckout bookCheckout)
    {
        var update_check = Read(bookCheckout.Id) ?? throw new KeyNotFoundException($"Запись о выдаче с Id {bookCheckout.Id} не найдена.");

        update_check.Reader = bookCheckout.Reader;
        update_check.Book = bookCheckout.Book;
        update_check.LoanDate = bookCheckout.LoanDate;
        update_check.LoanDays = bookCheckout.LoanDays;
        
    }

    /// <summary>
    /// Удаляет запись о выдаче по Id.
    /// </summary>
    /// <param name="id"> Id записи, которую нужно удалить. </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если запись с указанным Id не найдена. </exception>
    public void Delete(int id) {
        var deleted_check = Read(id) ?? throw new KeyNotFoundException($"Запись о выдаче с Id {id} не найдена");
        _bookCheckouts.Remove(deleted_check);
    }

}