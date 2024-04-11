namespace Chat.Application.Interfaces.Persistence;

public interface IConversationRepository<TEntity, TRepository> : IRepository<TEntity, TRepository> where TEntity : class
{
    Task<List<TEntity>?> GetAllByUserIdAsync(Guid userId);
}