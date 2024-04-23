namespace Chat.Application.Interfaces.Persistence;

public interface IConversationRepository<TEntity, TResponse> : IEntityRepository<TEntity> where TEntity : class
{
    Task<List<TResponse>?> GetAllByUserIdAsync(Guid userId);
    Task<(bool success, string message)> AddParticipants(Guid id, IEnumerable<Guid> participantIds, bool saveChanges = false);
}