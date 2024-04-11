using Chat.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using UserModel = Chat.Domain.User.User;

namespace Chat.Infrastructure.Persistence.User;

internal class UserRepository : IRepository<UserModel, UserRepository>
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

    public async Task<UserModel> AddAsync(UserModel entity)
    {
        var entityEntry = await _dbContext.Users.AddAsync(entity);
        return entityEntry.Entity;
    }

    public bool Update(UserModel entity)
    {
        var entityEntry = _dbContext.Users.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }

    public bool Delete(Guid id)
    {
        var entityEntry = _dbContext.Users.Remove(new UserModel { Id = id });
        return entityEntry.State == EntityState.Deleted;
    }
}