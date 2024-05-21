using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        var userModel = new ParticipantResponse(user);
        return Ok(userModel);
    }
    
    [Authorize]
    [HttpGet]
    [Route("search/{name}")]
    [ProducesResponseType<List<ParticipantResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser(string name)
    {
        var users = await _userManager.Users
            .Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name))
            .Select(u => new ParticipantResponse(u))
            .ToListAsync();

        if (users.Count == 0)
        {
            return NotFound($"User with name: {name} not found");
        }

        return Ok(users);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequest registrationModel)
    {
        var registrationStatus = await _loginService.Register(registrationModel);

        return registrationStatus.Success ? Ok(registrationStatus) : BadRequest(registrationStatus);
    }
}