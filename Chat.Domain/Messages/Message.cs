namespace Chat.Domain.Messages;

public class Message
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ConversationId { get; set; }
    public string UserId { get; set; } = default!;
    public DateTime SentAt { get; set; }
    public string? Content { get; set; }
}