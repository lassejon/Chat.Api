namespace Chat.Application.Responses
{
    public class ConversationsResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required List<ParticipantResponse> Participants { get; set; }
        public required string LatestMessage { get; set; }
        public required DateTime LatestMessageAt { get; set; }
    }
}
