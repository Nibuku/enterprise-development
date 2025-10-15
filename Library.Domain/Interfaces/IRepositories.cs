namespace Library.Domain.Interfaces;
public interface IRepositories<TEntity, TKey>
{
    public void Create(TEntity entity);

    public void Update(TEntity entity);

    public void Delete(TKey key);

    public List<TEntity> ReadAll();

    public TEntity? Read(TKey key);

}
