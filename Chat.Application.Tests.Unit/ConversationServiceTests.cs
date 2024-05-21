using Chat.Application.Interfaces;
using Chat.Application.Interfaces.Persistence;
using Chat.Application.Requests;
using Chat.Application.Responses;
using Chat.Application.Results;
using Chat.Application.Services;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Conversations;
using Chat.Domain.Messages;
using FluentAssertions;
using NSubstitute;

namespace Chat.Application.Tests.Unit;

public class ConversationServiceTests
{
    private readonly ConversationService _systemUnderTest;
    private readonly IConversationRepository<Conversation, ConversationsResponse> _conversationRepository = Substitute.For<IConversationRepository<Conversation, ConversationsResponse>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IEntityRepository<Message> _messageRepository = Substitute.For<IEntityRepository<Message>>();
    
    public ConversationServiceTests()
    {
        _systemUnderTest = new ConversationService(_conversationRepository, _unitOfWork, _messageRepository);
    }
    
    [Fact]
    public async Task AddParticipants_ShouldReturnFailure_WhenNoConversationIsFound()
    {
        // Arrange
        var participents = new  List<Guid>{  Guid.NewGuid(), Guid.NewGuid() };
        var conversationId = Guid.NewGuid();
        _conversationRepository.AddParticipants(Arg.Any<Guid>(), Arg.Any<IEnumerable<Guid>>(), Arg.Any<bool>()).Returns((false, "Error message"));

        // Act
        var result = await _systemUnderTest.AddParticipantsAsync(conversationId, participents);

        // Assert
        result.Should().BeEquivalentTo(new Result(false, "Error message"));
    }
    
    [Fact]
    public async Task AddParticipants_ShouldReturnSuccess_WhenParticipantsAreAdded()
    {
        // Arrange
        var participents = new  List<Guid>{  Guid.NewGuid(), Guid.NewGuid() };
        var conversationId = Guid.NewGuid();
        _conversationRepository.AddParticipants(Arg.Any<Guid>(), Arg.Any<IEnumerable<Guid>>(), Arg.Any<bool>()).Returns((true, "Participants added successfully"));

        // Act
        var result = await _systemUnderTest.AddParticipantsAsync(conversationId, participents);

        // Assert
        result.Should().BeEquivalentTo(new Result(true, "Participants added successfully"));
    }
    
    [Fact]
    public async Task CreateConversationAsync_ShouldReturnConversationResponse_WhenConversationIsCreated()
    {
        // Arrange
        var conversationRequest = new ConversationRequest(new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }, "Test conversation", new MessageRequest(Guid.NewGuid(),"Test message", Guid.NewGuid()));
        var conversation = conversationRequest.ToConversation();
        _conversationRepository.AddAsync(Arg.Any<Conversation>(), Arg.Any<bool>()).Returns(conversation);

        // Act
        var result = await _systemUnderTest.CreateConversationAsync(conversationRequest);

        // Assert
        result.Should().BeEquivalentTo(new ConversationResponse(conversation.Id, conversation.Name, conversation.Messages.Select(m => new MessageResponse(m)), conversation.Participants.Select(p => new ParticipantResponse(p))));
    }
    
    [Fact]
    public async Task AddMessageAsync_ShouldReturnMessage_WhenMessageIsAdded()
    {
        // Arrange
        var messageRequest = new MessageRequest(Guid.NewGuid(), "Test message", Guid.NewGuid());
        var message = messageRequest.ToMessage();
        _messageRepository.AddAsync(Arg.Any<Message>()).Returns(message);

        // Act
        var result = await _systemUnderTest.AddMessageAsync(messageRequest);

        // Assert
        result.Should().BeEquivalentTo(message);
    }
    
    [Fact]
    public async Task GetConversationByIdAsync_ShouldReturnConversationResponse_WhenConversationIsFound()
    {
        // Arrange
        var conversationId = Guid.NewGuid();
        var conversation = new Conversation { Id = conversationId, Name = "Test conversation", CreatedAt = DateTime.UtcNow };
        _conversationRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(conversation);

        // Act
        var result = await _systemUnderTest.GetConversationByIdAsync(conversationId);

        // Assert
        result.Should().BeEquivalentTo(new ConversationResponse(conversation));
    }
    
    [Fact]
    public async Task DeleteConversationAsync_ShouldReturnTrue_WhenConversationIsDeleted()
    {
        // Arrange
        var conversationId = Guid.NewGuid();
        _conversationRepository.Delete(Arg.Any<Guid>(), Arg.Any<bool>()).Returns(true);

        // Act
        var result = await _systemUnderTest.DeleteConversationAsync(conversationId);

        // Assert
        result.Should().BeTrue();
    }
}