using Chat.Application.Interfaces;
using Chat.Application.Interfaces.Persistence;
using Chat.Application.Requests;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Conversations;
using Chat.Domain.Messages;

namespace Chat.Application.Services;

public class ConversationService : IConversationService
{
    private readonly IConversationRepository<Conversation> _conversationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Message> _messageRepository;

    public ConversationService(IConversationRepository<Conversation> conversationRepository, IUnitOfWork unitOfWork, IRepository<Message> messageRepository)
    {
        _conversationRepository = conversationRepository;
        _unitOfWork = unitOfWork;
        _messageRepository = messageRepository;
    }
    
    public async Task<ConversationResponse> CreateConversationAsync(ConversationRequest conversationRequest)
    {
        var conversation = await _conversationRepository.AddAsync(conversationRequest.ToConversation(), saveChanges: true);

        return new ConversationResponse(conversation.Id, conversation.CreatedAt);
    }
    
    public async Task AddParticipantsAsync(Guid id, IEnumerable<Guid> participantIds)
    {
        await _conversationRepository.AddParticipants(id, participantIds, saveChanges: true);
    }
    
    public async Task AddMessageAsync(Guid id, MessageRequest messageRequest)
    {
        var message = messageRequest.ToMessage();
        message.ConversationId = id;
        
        var messageAdded = await _messageRepository.AddAsync(message);
        await _unitOfWork.CommitChangesAsync();
    }
    
    public async Task<Conversation?> GetConversationByIdAsync(Guid id)
    {
        var conversation = await _conversationRepository.GetByIdAsync(id);

        return conversation;
    }
    
    public async Task<List<Conversation>?> GetConversationsByUserIdAsync(Guid userId)
    {
        var conversations = await _conversationRepository.GetAllByUserIdAsync(userId);

        return conversations;
    }
}