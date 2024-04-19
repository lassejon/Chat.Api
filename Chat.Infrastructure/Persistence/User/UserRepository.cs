using Chat.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using UserModel = Chat.Domain.User.User;

namespace Chat.Infrastructure.Persistence.User;

internal class UserRepository : IRepository<UserModel>
{
    private readonly ChatDbContext _dbContext;

    public UserRepository(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UserModel?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<List<UserModel>?> ListAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<UserModel> AddAsync(UserModel entity, bool saveChanges = false)
    {
        var entityEntry = await _dbContext.Users.AddAsync(entity);
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.Entity;
    }

    public async Task<bool> Update(UserModel entity, bool saveChanges = false)
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
        var entityEntry = _dbContext.Users.Remove(new UserModel { Id = id.ToString() });
        
        if (saveChanges)
        {
            await _dbContext.SaveChangesAsync();
        }
        
        return entityEntry.State == EntityState.Deleted;
    }
}