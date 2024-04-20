using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Chat.Domain.Users;

namespace Chat.Infrastructure.Persistence.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
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