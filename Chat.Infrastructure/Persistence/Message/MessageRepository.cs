using Chat.Application.Interfaces;

namespace Chat.Infrastructure.Persistence.Message;

public class MessageRepository : IRepository<Domain.Message.Message, MessageRepository>
{
    public Task<Domain.Message.Message> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.Message.Message>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Message.Message> AddAsync(Domain.Message.Message entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Message.Message entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}