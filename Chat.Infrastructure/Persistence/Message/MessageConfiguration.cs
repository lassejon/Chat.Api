using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MessageModel = Chat.Domain.Message.Message;
using UserModel = Chat.Domain.User.User;

namespace Chat.Infrastructure.Persistence.Message
{
    public class MessageConfiguration : IEntityTypeConfiguration<MessageModel>
    {
        public void Configure(EntityTypeBuilder<MessageModel> builder)
        {
            builder
                .HasOne<UserModel>()
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .IsRequired();
        }
    }
}
