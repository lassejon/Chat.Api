using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ConversationModel = Chat.Domain.Conversation.Conversation;
using UserModel = Chat.Domain.User.User;

namespace Chat.Infrastructure.Persistence.Conversation;

public class ConversationConfiguration : IEntityTypeConfiguration<ConversationModel>
{
    public void Configure(EntityTypeBuilder<ConversationModel> builder)
    {
        builder
            .HasMany<UserModel>()
            .WithMany(e => e.Conversations)
            .UsingEntity("Participants");
    }
}