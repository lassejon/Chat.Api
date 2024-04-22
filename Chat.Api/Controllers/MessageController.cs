using Chat.Application.Extensions;
using Chat.Application.Requests;
using Chat.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]s")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateConversationAsync([FromBody] MessageRequest message)
    {
        var result = await _messageService.CreateMessageAsync(message);

        return result.Match<ObjectResult>(
            onSuccess: r => Ok(r.Message),
            onFailure: r => BadRequest(r.Message));
    }
}