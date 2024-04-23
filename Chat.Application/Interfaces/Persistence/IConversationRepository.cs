namespace Chat.Application.Interfaces.Persistence;

public interface IConversationRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>?> GetAllByUserIdAsync(Guid userId);
    Task<(bool success, string message)> AddParticipants(Guid id, IEnumerable<Guid> participantIds, bool saveChanges = false);
}