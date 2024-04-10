using Chat.Application.Interfaces;

namespace Chat.Infrastructure.Persistence.User;

public class UserRepository : IRepository<Domain.User.User, UserRepository>
{
    public Task<Domain.User.User> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.User.User>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.User.User> AddAsync(Domain.User.User entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.User.User entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}