using Chat.Application.Requests;
using Chat.Application.Responses;

namespace Chat.Application.Services;

public interface ILoginService
{
    Task<JwtTokenResponse> TryLogin(LoginRequest model);
    Task<RegistrationResponse> Register(RegistrationRequest model, bool retry = true, int retries = 10, int trie = 0);
}