using Chat.Application.Interfaces.Persistence;
using Chat.Domain.Messages;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Persistence.Messages;

internal class MessageRepository(ChatDbContext dbContext) : IRepository<Message>
{
    public async Task<Message?> GetByIdAsync(Guid id)
    {
        return await dbContext.Messages.FindAsync(id);
    }

    public async Task<List<Message>?> ListAsync()
    {
        return await dbContext.Messages.ToListAsync();
    }

    public async Task<Message> AddAsync(Message entity, bool saveChanges = false)
    {
        var entityEntry = await dbContext.Messages.AddAsync(entity);
        
        if (saveChanges)
        {
            await dbContext.SaveChangesAsync();
        }
        
        return entityEntry.Entity;
    }

    public async Task<bool> Update(Message entity, bool saveChanges = false)
    {
        var entityEntry = dbContext.Messages.Update(entity);
        
        if (saveChanges)
        {
            await dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<bool> Delete(Guid id, bool saveChanges = false)
    {
        var entityEntry = dbContext.Messages.Remove(new Message { Id = id });
        
        if (saveChanges)
        {
            await dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Deleted;
    }
}