using Chat.Application.Interfaces.Persistence;
using Chat.Domain.Conversations;
using Microsoft.EntityFrameworkCore;
using Chat.Domain.Messages;
using Chat.Domain.Users;

namespace Chat.Infrastructure.Persistence.Conversations;

internal class ConversationRepository : IConversationRepository<Conversation>
{
    private readonly ChatDbContext _dbContext;

    public ConversationRepository(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Conversation>?> GetAllByUserIdAsync(Guid id)
    {
        var conversations = await _dbContext.Conversations
            .Include(c => c.Participants)
            .Where(c => c.Participants.Contains(new Participant { UserId = id.ToString() }))
            .ToListAsync();

        return conversations;
    }

    public async Task AddParticipants(Guid id, IEnumerable<Guid> participantIds, bool saveChanges = false)
    {
        var participants = participantIds.Select(p => new Participant { UserId = p.ToString(), ConversationId = id });
        await _dbContext.Participants.AddRangeAsync(participants);

        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Conversation?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Conversations
            .Include(c => c.Participants)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Conversation>?> ListAsync()
    {
        return await _dbContext.Conversations.ToListAsync();
    }

    public async Task<Conversation> AddAsync(Conversation entity, bool saveChanges = false)
    {
        //var conversation = new Conversation(){ Id = entity.Id, Name = entity.Name };
      
        //await _dbContext.ConversationUser.AddRangeAsync(participants);
        //await _dbContext.Messages.AddRangeAsync(entity.Messages);
        var entityEntry = await _dbContext.Conversations.AddAsync(entity);

        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }

        return entityEntry.Entity;
    }

    public async Task<bool> Update(Conversation entity, bool saveChanges = false)
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
        var entity = new Conversation() { Id = id };

        var entityEntry = _dbContext.Conversations.Remove(entity);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Deleted;
    }
}