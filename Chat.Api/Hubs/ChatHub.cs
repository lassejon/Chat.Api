using Chat.Api.Extensions;
using Chat.Api.Hubs.Interfaces;
using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Hubs;

public class ChatHub : Hub<IChatClient>
{
    private readonly IConversationService _conversationService;

    public ChatHub(IConversationService conversationService)
    {
        _conversationService = conversationService;
    }
    public override async Task OnConnectedAsync()
    { 
        var userId = Context.User?.GetId();
        var conversations = await _conversationService.GetConversationsByUserIdAsync(userId ?? Guid.Empty);
        
        foreach (var conversation in conversations)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversation.Id.ToString());
        }
        
        await base.OnConnectedAsync();
    }
    
    public async Task<MessageResponse> SendMessage(MessageRequest messageRequest)
    {
        var message = await _conversationService.AddMessageAsync(messageRequest);
        var messageResponse = new MessageResponse(message); //new MessageResponse(Id: Guid.NewGuid(), Content: messageRequest.Content, SentAt: DateTime.UtcNow, UserId: messageRequest.UserId.ToString(), ConversationId: messageRequest.ConversationId);
        
        await Clients.OthersInGroup(messageRequest.ConversationId.ToString()).ReceiveMessage(messageResponse);

        return messageResponse;
    }
}