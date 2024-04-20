using Chat.Domain.Messages;
using Chat.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Persistence.Messages
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .IsRequired();
        }
    }
}
