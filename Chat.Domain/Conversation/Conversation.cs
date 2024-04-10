namespace Chat.Domain.Conversation;

public class Conversation
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Message.Message>? Messages { get; set; }
}