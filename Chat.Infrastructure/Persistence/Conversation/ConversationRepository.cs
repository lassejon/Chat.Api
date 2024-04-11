using Chat.Application.Interfaces;
using Chat.Domain.Conversation;

namespace Chat.Infrastructure.Persistence.Conversations;

internal class ChatRepository : IRepository<Conversation, ChatRepository>
{
    public Task<Conversation> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Conversation>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Conversation> AddAsync(Conversation entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Conversation entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}