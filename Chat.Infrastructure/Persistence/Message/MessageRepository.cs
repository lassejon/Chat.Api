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

    public async Task<MessageModel> AddAsync(MessageModel entity)
    {
        var entityEntry = await _dbContext.Messages.AddAsync(entity);
        
        return entityEntry.Entity;
    }

    public bool Update(MessageModel entity)
    {
        var entityEntry = _dbContext.Messages.Update(entity);
        
        return entityEntry.State == EntityState.Modified;
    }

    public bool Delete(Guid id)
    {
        var entityEntry = _dbContext.Messages.Remove(new MessageModel { Id = id });
        
        return entityEntry.State == EntityState.Deleted;
    }
}