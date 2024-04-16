using Microsoft.AspNetCore.Identity;

namespace Chat.Domain.User;

public class User : IdentityUser
{
    public int Index { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    public DateTime BirthDay { get; set; }
    public List<Conversation.Conversation>? Conversations { get; set; }
}