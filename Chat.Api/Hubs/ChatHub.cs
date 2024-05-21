using Chat.Api.Extensions;
using Chat.Api.Hubs.Interfaces;
using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Users;
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
        
        await Groups.AddToGroupAsync(Context.ConnectionId, userId?.ToString() ?? Guid.Empty.ToString());
        
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

    public async Task<ConversationResponse> CreateConversation(ConversationRequest conversationRequest)
    {
        // var result = await _conversationService.CreateConversationAsync(conversationRequest);
        var participants = conversationRequest.ParticipantIds.Select(id => new ParticipantResponse(Id: id.ToString(), FirstName: "Random", LastName: "User"));
        var result = new ConversationResponse(Id: Guid.NewGuid(), Name: conversationRequest.Name, Participants: participants, Messages: new List<MessageResponse>());
        
        foreach (var participantId in conversationRequest.ParticipantIds)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, participantId.ToString());
        }

        await Clients.OthersInGroup(result.Id.ToString()).ReceiveConversation(result);
        
        return result;
    }
    
    public async Task<bool> DeleteConversation(Guid conversationId)
    {
        var deleted = await _conversationService.DeleteConversationAsync(conversationId);
        
        if (deleted)
        {
            await Clients.OthersInGroup(conversationId.ToString()).DeletedConversation(conversationId);
        }
        
        return deleted;
    }
}