using Chat.Domain.Users;

namespace Chat.Domain.Conversations;

public class Participant
{
    public required string UserId { get; set; }

    public Guid ConversationId { get; set; }
    public User User { get; set; } = null!;
    public Conversation Conversation { get; set; } = null!;
}