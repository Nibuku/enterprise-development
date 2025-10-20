namespace Library.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для CRUD-операций с объектами доменной области.
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип Id сущности</typeparam>
public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Создает новый объект.
    /// </summary>
    /// <param name="entity">Создаваемый объект</param>
    public TKey Create(TEntity entity);

    /// <summary>
    /// Обновляет существующий объект.
    /// </summary>
    /// <param name="entity">Обновленный объект</param>
    public TEntity? Update(TEntity entity);

    /// <summary>
    /// Удаляет объект по указанному Id.
    /// </summary>
    /// <param name="key">Id удаляемого объекта</param>
    public bool Delete(TKey key);

    /// <summary>
    /// Получает список всех существующих объектов
    /// </summary>
    /// <returns>Список всех объектов</returns>
    public List<TEntity> ReadAll();

    /// <summary>
    /// Получает объект по Id.
    /// </summary>
    /// <param name="key">Id объекта</param>
    /// <returns>Найденный объект или null.</returns>
    public TEntity? Read(TKey key);
}
