using Chat.Domain.Conversations;

namespace Chat.Application.Responses
{
    public record ConversationResponse(Guid Id, string Name, IEnumerable<MessageResponse> Messages, IEnumerable<ParticipantResponse> Participants)
    {
        public ConversationResponse(Conversation conversation) : this(conversation.Id, conversation.Name, conversation.Messages.Select(m => new MessageResponse(m)), conversation.Participants.Select(p => new ParticipantResponse(p))) { }
    }
}
