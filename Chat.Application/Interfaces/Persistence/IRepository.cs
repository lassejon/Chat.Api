namespace Chat.Application.Interfaces.Persistence;

public interface IRepository<TEntity, TRepository> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>?> ListAsync();
    Task<TEntity> AddAsync(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(Guid id);
}