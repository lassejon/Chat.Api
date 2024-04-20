using Chat.Domain.Messages;
using Chat.Domain.Users;

namespace Chat.Domain.Conversations;

public class Conversation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public List<Message> Messages { get; set; } = [];
    public List<Participant> Participants { get; set; } = [];
    //public List<ConversationUser> ConversationUsers { get; set; } = [];
    public DateTime CreatedAt { get; set; }
}