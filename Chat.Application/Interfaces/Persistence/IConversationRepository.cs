namespace Chat.Application.Interfaces.Persistence;

public interface IConversationRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>?> GetAllByUserIdAsync(Guid userId);
}