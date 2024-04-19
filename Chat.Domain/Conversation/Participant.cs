namespace Chat.Domain.Conversation;

public class Participant
{
    public Guid UserId { get; set; }
    public Guid ConversationId { get; set; }
}