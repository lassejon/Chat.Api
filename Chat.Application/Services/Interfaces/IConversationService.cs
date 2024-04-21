using Chat.Application.Requests;
using Chat.Application.Results;
using Chat.Domain.Conversations;
using Chat.Domain.Messages;
using Chat.Domain.Users;

namespace Chat.Application.Services.Interfaces;

public interface IConversationService
{
    Task<ConversationResponse> CreateConversationAsync(ConversationRequest conversationRequest);
    Task<Result> AddParticipantsAsync(Guid id, IEnumerable<Guid> participantIds);
    Task AddMessageAsync(Guid id, MessageRequest messageRequest);
    Task<List<Conversation>?> GetConversationsByUserIdAsync(Guid userId);
    Task<Conversation?> GetConversationByIdAsync(Guid id);
}

public record ConversationResponse(Guid Id, DateTime CreatedAt);

public record ConversationRequest(List<Guid> ParticipantIds, string Name, MessageRequest Message)
{
    public Conversation ToConversation()
    {
        var conversation = new Conversation { Name = Name, CreatedAt = DateTime.UtcNow };
        conversation.Messages.Add(Message.ToMessage());
        conversation.Participants.AddRange(ParticipantIds.Select(id => new User { Id = id.ToString() }));

        return conversation;
    }
};

