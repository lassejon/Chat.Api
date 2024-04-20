using UserModel = Chat.Domain.User.User; 
namespace Chat.Domain.Conversation;

public class Participant
{
    public string UserId { get; set; }
    public Guid ConversationId { get; set; }
}