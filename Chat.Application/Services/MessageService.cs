using Chat.Application.Interfaces.Persistence;
using Chat.Application.Requests;
using Chat.Application.Results;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Messages;

namespace Chat.Application.Services;

public class MessageService(IRepository<Message> messageRepository) : IMessageService
{
    private readonly IRepository<Message> _messageRepository = messageRepository;

    public async Task<Result> CreateMessageAsync(MessageRequest messageRequest)
    {
        var message = await _messageRepository.AddAsync(messageRequest.ToMessage(), true);
        
        return new Result(true, "Message created successfully");
    }
}