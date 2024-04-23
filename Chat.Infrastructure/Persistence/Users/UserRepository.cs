using Chat.Application.Interfaces.Persistence;
using Chat.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Persistence.Users;

internal class UserRepository : IEntityRepository<User>
{
    private readonly ChatDbContext _dbContext;

    public UserRepository(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<List<User>?> ListAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User> AddAsync(User entity, bool saveChanges = false)
    {
        var entityEntry = await _dbContext.Users.AddAsync(entity);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.Entity;
    }

    public async Task<bool> Update(User entity, bool saveChanges = false)
    {
        var entityEntry = _dbContext.Users.Update(entity);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<bool> Delete(Guid id, bool saveChanges = false)
    {
        var entityEntry = _dbContext.Users.Remove(new User { Id = id.ToString() });
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Deleted;
    }
}