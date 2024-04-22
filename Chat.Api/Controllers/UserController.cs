using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class UserController(UserManager<User> userManager, ILoginService loginService) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ILoginService _loginService = loginService;

    [Authorize]
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null)
        {
            return NotFound($"User with id: {id} not found");
        }

        var userModel = new UserResponse(user);
        return Ok(userModel);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequest registrationModel)
    {
        var registrationStatus = await _loginService.Register(registrationModel);

        return registrationStatus.Success ? Ok(registrationStatus) : BadRequest(registrationStatus);
    }
}