using Chat.Domain.Messages;

namespace Chat.Application.Requests;

public record MessageRequest(Guid UserId, string Content, Guid ConversationId)
{
    public Message ToMessage()
    {
        return new Message() { UserId = UserId.ToString(), ConversationId = ConversationId, Content = Content, SentAt = DateTime.UtcNow };
    }
}