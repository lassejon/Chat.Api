using Chat.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using MessageModel = Chat.Domain.Message.Message;

namespace Chat.Infrastructure.Persistence.Message;

internal class MessageRepository : IRepository<MessageModel>
{
    private readonly ChatDbContext _dbContext;

    public MessageRepository(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<MessageModel?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Messages.FindAsync(id);
    }

    public async Task<List<MessageModel>?> ListAsync()
    {
        return await _dbContext.Messages.ToListAsync();
    }

    public async Task<MessageModel> AddAsync(MessageModel entity, bool saveChanges = false)
    {
        var entityEntry = await _dbContext.Messages.AddAsync(entity);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.Entity;
    }

    public async Task<bool> Update(MessageModel entity, bool saveChanges = false)
    {
        var entityEntry = _dbContext.Messages.Update(entity);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<bool> Delete(Guid id, bool saveChanges = false)
    {
        var entityEntry = _dbContext.Messages.Remove(new MessageModel { Id = id });
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Deleted;
    }
}