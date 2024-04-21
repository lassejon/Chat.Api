using Chat.Domain.Conversations;
using Microsoft.AspNetCore.Identity;

namespace Chat.Domain.Users;

public class User : IdentityUser
{
    public int Index { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    //public List<Conversation> Conversations { get; set; } = [];
    public List<Conversation> Conversations { get; set; } = [];
}