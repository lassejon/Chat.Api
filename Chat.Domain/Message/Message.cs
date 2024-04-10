namespace Chat.Domain.Message;

public class Message
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public DateTime SentAt { get; set; }
    public string? Content { get; set; }
}