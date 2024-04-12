using Chat.Application.Interfaces;
using Chat.Application.Interfaces.Persistence;
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
            .FindAsync(id);

        if (user is null)
        {
            
        }

        return user!.Conversations;
    }

    public async Task<ConversationModel?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Conversations.FindAsync(id);
    }

    public async Task<List<ConversationModel>?> ListAsync()
    {
        return await _dbContext.Conversations.ToListAsync();
    }

    public async Task<ConversationModel> AddAsync(ConversationModel entity)
    {
        var entityEntry = await _dbContext.Conversations.AddAsync(entity);

        return entityEntry.Entity;
    }

    public bool Update(ConversationModel entity)
    {
        var entityEntry =  _dbContext.Conversations.Update(entity);

        return entityEntry.State == EntityState.Modified;
    }

    public bool Delete(Guid id)
    {
        var entity = new ConversationModel() { Id = id };

        var entityEntry = _dbContext.Conversations.Remove(entity);
        
        return entityEntry.State == EntityState.Deleted;
    }
}