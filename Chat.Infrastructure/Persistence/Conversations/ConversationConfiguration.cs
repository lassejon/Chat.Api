using Chat.Domain.Conversations;
using Chat.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Persistence.Conversations;

public class ConversationConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder
            .HasKey(p => new { p.UserId, p.ConversationId });

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