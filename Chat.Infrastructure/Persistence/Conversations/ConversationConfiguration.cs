using Chat.Domain.Conversations;
using Chat.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Persistence.Conversations;

public class ConversationConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(c => c.Conversations)
            .WithMany(u => u.Participants)
            .UsingEntity("Participants");

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