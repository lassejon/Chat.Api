using Chat.Domain.Message;

namespace Chat.Application.Requests;

public record MessageRequest(Guid UserId, string Content)
{
    public Message ToMessage()
    {
        return new Message() { Id = Guid.NewGuid(), UserId = UserId, Content = Content, SentAt = DateTime.UtcNow };
    }
}