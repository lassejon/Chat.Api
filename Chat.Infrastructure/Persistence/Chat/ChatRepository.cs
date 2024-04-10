using Chat.Application.Interfaces;

namespace Chat.Infrastructure.Persistence.Chat;

public class ChatRepository : IRepository<Domain.Chat.Chat, ChatRepository>
{
    public Task<Domain.Chat.Chat> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Domain.Chat.Chat>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Chat.Chat> AddAsync(Domain.Chat.Chat entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Chat.Chat entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}