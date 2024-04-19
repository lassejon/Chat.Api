using Chat.Application.Requests;
using Chat.Domain.Conversation;
using Chat.Domain.Message;
using Chat.Domain.User;

namespace Chat.Application.Services.Interfaces;

public interface IConversationService
{
    Task<ConversationResponse> CreateConversationAsync(ConversationRequest conversationRequest);
    Task AddParticipantsAsync(Guid id, IEnumerable<Guid> participantIds);
    Task AddMessageAsync(Guid id, MessageRequest messageRequest);
    Task<List<Conversation>?> GetConversationsByUserIdAsync(Guid userId);
    Task<Conversation?> GetConversationByIdAsync(Guid id);
}

public record ConversationResponse(Guid Id, DateTime CreatedAt);

public record ConversationRequest(List<Guid> ParticipantIds, string Name, MessageRequest Message)
{
    public Conversation ToConversation()
    {
        var conversation = new Conversation { Id = Guid.NewGuid(), Name = Name, CreatedAt = DateTime.UtcNow };
        conversation.Messages.Add(Message.ToMessage());
        conversation.Participants.AddRange(ParticipantIds.Select(p => new User { Id = p.ToString() }));

        return conversation;
    }
};