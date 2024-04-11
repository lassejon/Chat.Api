namespace Chat.Application.Interfaces;

public interface IRepository<TEntity, TRepository> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>?> ListAsync();
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
}