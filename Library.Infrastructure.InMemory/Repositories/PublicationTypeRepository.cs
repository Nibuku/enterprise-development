using Library.Domain.Interfaces;
using Library.Domain.Models;
using Library.Domain.Data;

namespace Library.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий с CRUD-операциями для PublicationType.
/// Использует данные из DataSeed.
/// </summary>
public class PublicationTypeRepository : IRepositoryAsync<PublicationType, int> 
{
    private readonly List<PublicationType> _types;
    private int _maxId;

    /// <summary>
    /// Конструктор для репозитория, использует начальные данные из DataSeed 
    /// и определяе максимальный текущий ID, для добавления новых объктов.
    /// </summary>
    public PublicationTypeRepository()
    {
        _types = [.. DataSeed.PublicationTypes];
        _maxId = _types.Count > 0 ? _types.Max(r => r.Id) : 0;
    }

    /// <summary>
    /// Вносит новый тип публикации.
    /// Генерирует новый Id и добавляет запись в коллекцию.
    /// </summary>
    /// <param name="type"> Объект PublicationType. </param>
    /// <returns> Id созданного типа публикации.</returns>
    public Task<int> Create(PublicationType type)
    {
        type.Id = ++_maxId;
        _types.Add(type);
        return Task.FromResult(type.Id);
    }

    /// <summary>
    /// Обновляет существующий тип публикации.
    /// </summary>
    /// <param name="type"> Обновленный объект PublicationType </param>
    /// <returns> Обновленный объект, или null, если тип публикации не найден.</returns>
    public async Task<PublicationType?> Update(PublicationType type)
    {
        var update_type =await Read(type.Id);
        if (update_type == null)
            return null;

        update_type.Type = type.Type;
        return update_type;
    }

    /// <summary>
    /// Метод возвращает тип публикации по заданному Id.
    /// </summary>
    /// <returns>Объект PublicationType.</returns>
    public Task<PublicationType?> Read(int id)
    {
        return Task.FromResult(_types.FirstOrDefault(t => t.Id == id));
    }

    /// <summary>
    /// Метод возвращает список всех типой публикаций.
    /// </summary>
    /// <returns> Список всех объектов PublicationType.</returns>
    public Task<List<PublicationType>> ReadAll() 
    {
        return Task.FromResult<List<PublicationType>>([.._types]); 
    }

    /// <summary>
    /// Удаляет тип публикации по Id.
    /// </summary>
    /// <param name="id"> Id типа, который нужно удалить. </param>
    /// <returns>Результат удаления.</returns>
    public async Task<bool> Delete(int id)
    {
        var deleted_type = await Read(id);
        if (deleted_type == null)
            return false;

        return _types.Remove(deleted_type);
    }
}
