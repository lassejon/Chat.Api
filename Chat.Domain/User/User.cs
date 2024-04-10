namespace Chat.Domain.User;

public class User
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<Chat.Chat>? Chats { get; set; }
}