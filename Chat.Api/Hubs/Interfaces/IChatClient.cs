using Chat.Application.Responses;

namespace Chat.Api.Hubs.Interfaces;

public interface IChatClient
{
    Task ReceiveMessage(MessageResponse message);
    Task ReceiveConversation(ConversationResponse conversation);
    Task DeletedConversation(Guid conversationId);
}