using System.Reflection;
using Chat.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Persistence;

public class ChatDbContext : DbContext, IUnitOfWork
{
    public DbSet<Domain.User.User> Users { get; set; } = null!;
    public DbSet<Domain.Conversation.Conversation> Conversations { get; set; } = null!;
    public DbSet<Domain.Message.Message> Messages { get; set; } = null!;

    public ChatDbContext(DbContextOptions options) : base(options)
    {
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}