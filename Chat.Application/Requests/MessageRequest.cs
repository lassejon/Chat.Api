using Chat.Domain.Messages;

namespace Chat.Application.Requests;

public record MessageRequest(Guid UserId, string Content)
{
    public Message ToMessage()
    {
        return new Message() { Id = Guid.NewGuid(), UserId = UserId.ToString(), Content = Content, SentAt = DateTime.UtcNow };
    }
}