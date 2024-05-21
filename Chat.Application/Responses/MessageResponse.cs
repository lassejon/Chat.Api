using Chat.Domain.Messages;

namespace Chat.Application.Responses
{
    public record MessageResponse(Guid Id, string UserId, string Content, DateTime SentAt, Guid ConversationId)
    {
        public MessageResponse(Message message) : this(message.Id, message.UserId, message.Content!, message.SentAt, message.ConversationId) { }
    }
}