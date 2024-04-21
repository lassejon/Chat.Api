using Chat.Application.Attributes;

namespace Chat.Application.Requests;

public class RegistrationRequest
{
    [RequiredWithErrorMessage(nameof(Email))]
    public string? Email { get; set; }
    
    [RequiredWithErrorMessage(nameof(Password))]
    public string? Password { get; set; }
    
    [RequiredWithErrorMessage(nameof(FirstName))]
    public string? FirstName { get; set; }
    
    [RequiredWithErrorMessage(nameof(LastName))]
    public string? LastName { get; set; }
}