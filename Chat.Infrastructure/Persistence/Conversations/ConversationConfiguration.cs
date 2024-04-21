using Chat.Domain.Conversations;
using Chat.Domain.Joins;
using Chat.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Persistence.Conversations;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder
            .HasMany(c => c.Participants)
            .WithMany(u => u.Conversations)
            .UsingEntity(nameof(Participants),
                l => l.HasOne(typeof(User))
                    .WithMany()
                    .HasForeignKey(Participants.UserId)
                    .HasPrincipalKey(nameof(User.Id))
                    .OnDelete(DeleteBehavior.Cascade),
                r => r.HasOne(typeof(Conversation))
                    .WithMany()
                    .HasForeignKey(Participants.ConversationId)
                    .HasPrincipalKey(nameof(Conversation.Id))
                    .OnDelete(DeleteBehavior.Cascade),
                linkBuilder => linkBuilder.HasKey(Participants.UserId, Participants.ConversationId)
            );

        //builder
        //    .HasOne<User>()
        //    .WithMany()
        //    .HasForeignKey(p => p.UserId)
        //    .IsRequired();

        //builder
        //    .HasOne<Conversation>()
        //    .WithMany()
        //    .HasForeignKey(p => p.ConversationId)
        //    .IsRequired();
    }
}