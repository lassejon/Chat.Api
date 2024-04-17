using System.IdentityModel.Tokens.Jwt;
using Chat.Application.Requests;
using Chat.Application.Responses;

namespace Chat.Application.Services;

public interface ILoginService
{
    Task<(bool succes, JwtSecurityToken? token)> TryLogin(LoginRequest model);
    Task<RegistrationResponse> Register(RegistrationRequest model, bool retry = true, int retries = 10, int trie = 0);
}