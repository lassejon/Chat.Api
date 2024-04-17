using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserModel = Chat.Domain.User.User;

namespace Chat.Infrastructure.Persistence.User;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder
            .HasKey(u => u.Id)
            .IsClustered(false);
        
        builder
            .HasIndex(u => u.Index)
            .IsUnique()
            .IsClustered();
        
        builder
            .Property(u => u.Index)
            .ValueGeneratedOnAdd();
    }
}