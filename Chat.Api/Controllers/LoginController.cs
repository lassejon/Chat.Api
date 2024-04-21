using Chat.Application.Requests;
using Chat.Application.Services;
using Chat.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var jwtTokenResponse = await _loginService.TryLogin(loginRequest);

        if (!jwtTokenResponse.Success)
        {
            return Unauthorized();
        }

        return Ok(jwtTokenResponse);
    }
}