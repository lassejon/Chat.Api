using System.IdentityModel.Tokens.Jwt;
using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var (success, token) = await _loginService.TryLogin(loginRequest);

        if (!success)
        {
            return Unauthorized();
        }

        return Ok(new JwtTokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ValidTo = token!.ValidTo
        });
    }
}