using Chat.Api.Extensions;
using Chat.Application.Extensions;
using Chat.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]s")]
public class ConversationController : ControllerBase
{
    private readonly IConversationService _conversationService;

    public ConversationController(IConversationService conversationService)
    {
        _conversationService = conversationService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateConversationAsync([FromBody] ConversationRequest conversation)
    {
        var result = await _conversationService.CreateConversationAsync(conversation);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetConversationByIdAsync(Guid id)
    {
        var conversation = await _conversationService.GetConversationByIdAsync(id);
        return Ok(conversation);
    }

    [HttpPost("{id}/add-participants")]
    public async Task<IActionResult> AddParticipantsAsync(Guid id, [FromBody] IEnumerable<Guid> participants)
    {
        var result = await _conversationService.AddParticipantsAsync(id, participants);

        return result.Match<ObjectResult>(
                onSuccess: result => Ok(result.Message),
                onFailure: result => BadRequest(result.Message)
            );
    }

    [HttpGet]
    public async Task<IActionResult> GetConversations()
    {
        var id = User.GetId();

        if (id is null)
        {
            return BadRequest("User id not found");
        }
        
        var result = await _conversationService.GetConversationsByUserIdAsync(id.Value);

        return Ok(result);
    }
}