using Chat.Application.Interfaces.Persistence;
using Chat.Domain.Messages;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Persistence.Messages;

internal class MessageRepository : IRepository<Message>
{
    private readonly ChatDbContext _dbContext;

    public MessageRepository(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Message?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Messages.FindAsync(id);
    }

    public async Task<List<Message>?> ListAsync()
    {
        return await _dbContext.Messages.ToListAsync();
    }

    public async Task<Message> AddAsync(Message entity, bool saveChanges = false)
    {
        var entityEntry = await _dbContext.Messages.AddAsync(entity);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.Entity;
    }

    public async Task<bool> Update(Message entity, bool saveChanges = false)
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
        var entityEntry = _dbContext.Messages.Remove(new Message { Id = id });
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Deleted;
    }
}