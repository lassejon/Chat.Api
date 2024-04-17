using Chat.Application.Errors;

namespace Chat.Application.Requests;

public class LoginRequest
{
    [RequiredWithErrorMessage(nameof(Email))]
    public string? Email { get; set; }

    [RequiredWithErrorMessage(nameof(Password))]
    public string? Password { get; set; }
}