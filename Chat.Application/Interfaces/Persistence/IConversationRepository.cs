namespace Chat.Application.Interfaces.Persistence;

public interface IConversationRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>?> GetAllByUserIdAsync(Guid userId);
    Task AddParticipants(Guid id, IEnumerable<Guid> participantIds, bool saveChanges = false);
}