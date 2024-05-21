using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Results;
using Chat.Domain.Conversations;
using Chat.Domain.Messages;
using Chat.Domain.Users;

namespace Chat.Application.Services.Interfaces;

public interface IConversationService
{
    Task<ConversationResponse> CreateConversationAsync(ConversationRequest conversationRequest);
    Task<Result> AddParticipantsAsync(Guid id, IEnumerable<Guid> participantIds);
    Task<Message> AddMessageAsync(MessageRequest messageRequest);
    Task<List<ConversationsResponse>?> GetConversationsByUserIdAsync(Guid userId);
    Task<ConversationResponse?> GetConversationByIdAsync(Guid id);
    Task<bool> DeleteConversationAsync(Guid id);
}

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

