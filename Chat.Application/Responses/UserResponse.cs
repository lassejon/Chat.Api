using Chat.Domain.Users;

namespace Chat.Application.Responses;

public class UserResponse
{
    public UserResponse() { }
    
    public UserResponse(User user)
    {
        Id = Guid.Parse(user.Id);
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
    }
    
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}