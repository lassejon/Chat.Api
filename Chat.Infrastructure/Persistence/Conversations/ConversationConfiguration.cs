using Chat.Domain.Conversation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Persistence.Conversations;

public class ConversationConfiguration : IEntityTypeConfiguration<Domain.Conversation.Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder
            .HasMany<Domain.User.User>()
            .WithMany(e => e.Conversations)
            .UsingEntity("Participants");
    }
}