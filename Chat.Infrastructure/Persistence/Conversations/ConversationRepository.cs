using Chat.Application.Interfaces.Persistence;
using Chat.Domain.Conversations;
using Microsoft.EntityFrameworkCore;
using Chat.Domain.Users;
using Chat.Domain.Joins;

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
            .Include(c => c.Messages)
            .Where(c => c.Participants.Contains(new User { Id = id.ToString() }))
            .ToListAsync();

        return conversations;
    }

    public async Task<(bool success, string message)> AddParticipants(Guid id, IEnumerable<Guid> participantIds, bool saveChanges = false)
    {
        var rawSwl = $"INSERT INTO {nameof(Participants)} ({Participants.ConversationId}, {Participants.UserId}) VALUES {string.Join(",", participantIds.Select(userId => $"('{id}', '{userId}')"))}";

        try
        {
            _dbContext.Database.ExecuteSqlRaw(rawSwl);
            if (saveChanges)
            {
                await _dbContext.SaveChangesAsync();
            }

            return (true, "Participants added successfully");
        }
        catch (Exception e)
        {

            return (false, e.Message);
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
        _dbContext.Users.AttachRange(entity.Participants);
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