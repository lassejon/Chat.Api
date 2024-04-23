using Chat.Domain.Users;

namespace Chat.Application.Responses
{
    public record ParticipantResponse(string Id, string FirstName, string LastName)
    {
        public ParticipantResponse(User participant) : this(participant.Id, participant.FirstName!, participant.LastName!) { }
    }
}