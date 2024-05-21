using Chat.Application.Responses;

namespace Chat.Api.Hubs.Interfaces;

public interface IChatClient
{
    Task ReceiveMessage(MessageResponse message);
}