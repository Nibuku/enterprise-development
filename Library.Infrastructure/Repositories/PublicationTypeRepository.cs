using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для PublicationType.
/// Использует данные из DataSeed.
/// </summary>
public class PublicationTypeRepository : IRepositories<PublicationType, int> 
{
    private readonly List<PublicationType> _types;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public PublicationTypeRepository()
    {
        _types = DataSeed.PublicationTypes;
        _maxId = _types.Count > 0 ? _types.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Вносит новый тип публикации.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="type"> Объект PublicationType. </param>
    public void Create(PublicationType type)
    {
        type.Id = ++_maxId;
        _types.Add(type);
    }

    /// <summary>
    /// Обновляет существующий тип публикации.
    /// </summary>
    /// <param name="type"> Обновленный объект PublicationType </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если тип с указанным Id не найден. </exception>
    public void Update(PublicationType type)
    {
        var update_type = Read(type.Id) ?? throw new KeyNotFoundException($"Тип публикации с Id {type.Id} не найден.");
        update_type.Type = type.Type;
    }

    /// <summary>
    /// Метод возвращает тип издания по заданному Id.
    /// </summary>
    /// <returns>Объект PublicationType. </returns>
    public PublicationType? Read(int id)
    {
        return _types.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Метод возвращает все типы изданий.
    /// </summary>
    /// <returns> Список всех объектов PublicationType. </returns>
    public List<PublicationType> ReadAll() 
    {
        return _types; 
    }

    /// <summary>
    /// Удаляет тип публикации по Id.
    /// </summary>
    /// <param name="id"> Id типа, который нужно удалить. </param>
    /// <exception cref="KeyNotFoundException"> Вызывается, если тип с указанным Id не найден. </exception>
    public void Delete(int id)
    {
        var deleted_type = Read(id) ?? throw new KeyNotFoundException($"Type with ID {id} not found for delete.");
        _types.Remove(deleted_type);

    }
}
