using Chat.Domain.Message;

namespace Chat.Domain.Chat;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Message.Message>? MessagesTo { get; set; }
    public List<Message.Message>? MessagesFrom { get; set; }
}