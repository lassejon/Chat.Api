using Chat.Application.Services;
using Chat.Domain.Message;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversationController : ControllerBase
{
    private readonly EntityService _entityService;

    public ConversationController(EntityService entityService)
    {
        _entityService = entityService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateMessageAsync([FromBody] Message message)
    {
        var result = await _entityService.CreateMessageAsync(message);

        return result.Success ? Ok(result.Entity) : BadRequest();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMessageByIdAsync(Guid id)
    {
        var result = await _entityService.GetMessageByIdAsync(id);

        return result.Success ? Ok(result.Entity) : NotFound($"Message not found with id: {id}");
    }
}