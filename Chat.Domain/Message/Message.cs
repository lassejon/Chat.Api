namespace Chat.Domain.Message;

public class Message
{
    public Guid Id { get; set; }
    public Guid ConversationId { get; set; }
    public string UserId { get; set; } = default!;
    public DateTime SentAt { get; set; }
    public string? Content { get; set; }
}