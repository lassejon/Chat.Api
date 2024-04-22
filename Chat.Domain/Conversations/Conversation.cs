using Chat.Domain.Messages;
using Chat.Domain.Users;

namespace Chat.Domain.Conversations;

public class Conversation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public List<Message> Messages { get; set; } = [];
    public List<User> Participants { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}