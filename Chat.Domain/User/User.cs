namespace Chat.Domain.User;

public class User
{
    public Guid Id { get; set; }
    public List<Conversation.Conversation>? Conversations { get; set; }
}