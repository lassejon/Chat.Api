using Chat.Application.Interfaces;
using Chat.Application.Interfaces.Persistence;
using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Results;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Conversations;
using Chat.Domain.Messages;

namespace Chat.Application.Services;

public class ConversationService : IConversationService
{
    private readonly IConversationRepository<Conversation, ConversationsResponse> _conversationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEntityRepository<Message> _messageRepository;

    public ConversationService(IConversationRepository<Conversation, ConversationsResponse> conversationRepository, IUnitOfWork unitOfWork, IEntityRepository<Message> messageRepository)
    {
        _conversationRepository = conversationRepository;
        _unitOfWork = unitOfWork;
        _messageRepository = messageRepository;
    }
    
    public async Task<ConversationResponse> CreateConversationAsync(ConversationRequest conversationRequest)
    {
        var conversation = await _conversationRepository.AddAsync(conversationRequest.ToConversation(), saveChanges: true);

        return new ConversationResponse(conversation.Id, conversation.Name, conversation.Messages.Select(m => new MessageResponse(m)), conversation.Participants.Select(p => new ParticipantResponse(p)));
    }
    
    public async Task<Result> AddParticipantsAsync(Guid id, IEnumerable<Guid> participantIds)
    {
        var (success, error) = await _conversationRepository.AddParticipants(id, participantIds, saveChanges: true);

        return new Result(success, error);
    }
    
    public async Task<Message> AddMessageAsync(MessageRequest messageRequest)
    {
        var message = messageRequest.ToMessage();
        
        var messageAdded = await _messageRepository.AddAsync(message);
        await _unitOfWork.CommitChangesAsync();

        return messageAdded;
    }
    
    public async Task<ConversationResponse?> GetConversationByIdAsync(Guid id)
    {
        var conversation = await _conversationRepository.GetByIdAsync(id);

        return new ConversationResponse(conversation!);
    }
    
    public async Task<List<ConversationsResponse>?> GetConversationsByUserIdAsync(Guid userId)
    {
        var conversations = await _conversationRepository.GetAllByUserIdAsync(userId);

        return conversations;
    }
    
    public async Task<bool> DeleteConversationAsync(Guid id)
    {
        return await _conversationRepository.Delete(id, true);
    }
}