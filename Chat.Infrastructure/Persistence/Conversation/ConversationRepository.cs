using Chat.Application.Interfaces.Persistence;
using Chat.Domain.Conversation;
using Microsoft.EntityFrameworkCore;
using ConversationModel = Chat.Domain.Conversation.Conversation;

namespace Chat.Infrastructure.Persistence.Conversation;

internal class ConversationRepository : IConversationRepository<ConversationModel>
{
    private readonly ChatDbContext _dbContext;

    public ConversationRepository(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<ConversationModel>?> GetAllByUserIdAsync(Guid id)
    {
        var user = await _dbContext.Users
            .Include(u => u.Conversations)
            .FirstOrDefaultAsync(u => u.Id == id.ToString());

        if (user is null)
        {
            throw new ArgumentException($"User with ID {id} not found.");
        }

        return user!.Conversations;
    }

    public async Task AddParticipants(Guid id, IEnumerable<Guid> participantIds, bool saveChanges = false)
    {
        var participants = participantIds.Select(p => new Participant { UserId = p, ConversationId = id });
        await _dbContext.Participants.AddRangeAsync(participants);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<ConversationModel?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Conversations
            .Include(c => c.Participants)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<ConversationModel>?> ListAsync()
    {
        return await _dbContext.Conversations.ToListAsync();
    }

    public async Task<ConversationModel> AddAsync(ConversationModel entity, bool saveChanges = false)
    {
        var entityEntry = await _dbContext.Conversations.AddAsync(entity);

        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }

        return entityEntry.Entity;
    }

    public async Task<bool> Update(ConversationModel entity, bool saveChanges = false)
    {
        var entityEntry =  _dbContext.Conversations.Update(entity);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }

        return entityEntry.State == EntityState.Modified;
    }

    public async Task<bool> Delete(Guid id, bool saveChanges = false)
    {
        var entity = new ConversationModel() { Id = id };

        var entityEntry = _dbContext.Conversations.Remove(entity);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Deleted;
    }
}