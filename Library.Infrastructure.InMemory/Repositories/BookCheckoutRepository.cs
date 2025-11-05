using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для BookCheckout.
/// Использует данные из DataSeed.
/// </summary>
public class BookCheckoutRepository : IRepositoryAsync<BookCheckout, int>
{
    private readonly List<BookCheckout> _bookCheckouts;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public BookCheckoutRepository()
    {
        _bookCheckouts = [.. DataSeed.Checkouts];
        _maxId = _bookCheckouts.Count > 0 ? _bookCheckouts.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Метод возвращает все записи о выдаче книг.
    /// </summary>
    /// <returns> Список всех объектов BookCheckout. </returns>
    public Task<List<BookCheckout>> ReadAll()
    {
        return Task.FromResult<List<BookCheckout>>([.. _bookCheckouts]);
    }

    /// <summary>
    /// Метод возвращает запись о выдаче по заданному Id.
    /// </summary>
    /// <returns>Объект BookCheckout. </returns>
    public Task<BookCheckout?> Read(int id)
    {
        return Task.FromResult(_bookCheckouts.FirstOrDefault(r => r.Id == id));
    }

    /// <summary>
    /// Создает запись о выдаче.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="bookCheckout"> Объект BookCheckout. </param>
    /// <returns> Id созданной записи о выдаче.</returns>
    public Task<int> Create(BookCheckout bookCheckout)
    {
        bookCheckout.Id = ++_maxId;
        _bookCheckouts.Add(bookCheckout);
        return Task.FromResult(bookCheckout.Id);
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
        return update_check;
    }

    /// <summary>
    /// Удаляет запись о выдаче по Id.
    /// </summary>
    /// <param name="id"> Id записи, которую нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public async Task<bool> Delete(int id) {
        var deleted_check =await Read(id);
        if (deleted_check == null)
            return false;

        return _bookCheckouts.Remove(deleted_check);
    }
}