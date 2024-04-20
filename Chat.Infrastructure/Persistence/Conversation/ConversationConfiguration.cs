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
        builder
            .HasKey(p => new { p.UserId, p.ConversationId });

        builder
            .HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .IsRequired();

        builder
            .HasOne<ConversationModel>()
            .WithMany()
            .HasForeignKey(p => p.ConversationId)
            .IsRequired();
    }
}