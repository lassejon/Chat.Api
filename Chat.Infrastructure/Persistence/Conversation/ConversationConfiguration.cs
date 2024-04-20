using Chat.Domain.Conversation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ConversationModel = Chat.Domain.Conversation.Conversation;
using UserModel = Chat.Domain.User.User;

namespace Chat.Infrastructure.Persistence.Conversation;

public class ConversationConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(p => new { p.ConversationId, p.UserId });
        // builder
        //     .HasMany<UserModel>()
        //     .WithMany(e => e.Conversations)
        //     .UsingEntity(Participant);
    }
}