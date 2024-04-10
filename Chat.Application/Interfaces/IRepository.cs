namespace Chat.Application.Interfaces;

public interface IRepository<TEntity, TRepository> where TEntity : class
{
    Task<TEntity> GetByIdAsync(Guid id);
    Task<List<TEntity>> ListAsync();
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}