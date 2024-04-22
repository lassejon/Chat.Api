using Chat.Application.Requests;
using Chat.Application.Results;

namespace Chat.Application.Services.Interfaces;

public interface IMessageService
{
    Task<Result> CreateMessageAsync(MessageRequest messageRequest);
}