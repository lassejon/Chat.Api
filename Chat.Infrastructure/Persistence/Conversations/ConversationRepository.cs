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
        var user = await _dbContext.Users
            .Include(u => u.Conversations)
            .ThenInclude(c => c.Users)
            .FirstOrDefaultAsync(u => u.Id == id.ToString());

        if (user is null)
        {
            throw new ArgumentException($"User with ID {id} not found.");
        }

        return user.Conversations;
    }

    public async Task AddParticipants(Guid id, IEnumerable<Guid> participantIds, bool saveChanges = false)
    {
        //var participants = participantIds.Select(p => new ConversationUser { UserId = p.ToString(), ConversationId = id });
        //await _dbContext.ConversationUser.AddRangeAsync(participants);
        var conversation = await _dbContext.Conversations.FindAsync(id) ?? throw new ArgumentException($"Conversation with ID {id} not found.");
        
        conversation.Users.AddRange(participantIds.Select(p => new User { Id = p.ToString() }).ToList());
        
        _dbContext.Conversations.Update(conversation!);

        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Conversation?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Conversations
            .Include(c => c.Users)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Conversation>?> ListAsync()
    {
        return await _dbContext.Conversations.ToListAsync();
    }

    public async Task<Conversation> AddAsync(Conversation entity, bool saveChanges = false)
    {
        var conversation = new Conversation(){ Id = entity.Id, Name = entity.Name };
        //var participants = entity.Users.Select(p => new ConversationUser { UserId = p.Id.ToString(), ConversationId = entity.Id });
        var messages = entity.Messages.Select(m => new Message { Id = m.Id, Content = m.Content, SentAt = m.SentAt, UserId = m.UserId, ConversationId = entity.Id });
       
        //await _dbContext.ConversationUser.AddRangeAsync(participants);
        await _dbContext.Message.AddRangeAsync(messages);
        var entityEntry = await _dbContext.Conversations.AddAsync(conversation);

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