using Chat.Application.Interfaces;
using Chat.Application.Interfaces.Persistence;
using Chat.Domain.Message;

namespace Chat.Application.Services;

public class EntityService
{
    private readonly IRepository<Message> _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EntityService(IRepository<Message> messageRepository, IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Message>> CreateMessageAsync(Message message)
    {
        var result = new ErrorOr<Message>
        {
            Entity = await _messageRepository.AddAsync(message)
        };
        
        await _unitOfWork.CommitChangesAsync();

        return result;
    }
    
    public async Task<ErrorOr<Message>> GetMessageByIdAsync(Guid id)
    {
        var result = new ErrorOr<Message>
        {
            Entity = await _messageRepository.GetByIdAsync(id)
        };

        if (result.Entity is null)
        {
            result.Success = false;
        }
        
        return result;
    }
}

public class ErrorOr<TEntity> where TEntity : class
{
    public TEntity? Entity { get; set; }
    public bool Success { get; set; } = true;
}