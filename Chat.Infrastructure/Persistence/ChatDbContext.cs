using System.Reflection;
using Chat.Application.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserModel = Chat.Domain.User.User;
using ConversationModel = Chat.Domain.Conversation.Conversation;
using MessageModel = Chat.Domain.Message.Message;

namespace Chat.Infrastructure.Persistence;

public class ChatDbContext : IdentityDbContext<UserModel>, IUnitOfWork
{
    public override DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<ConversationModel> Conversations { get; set; } = null!;
    public DbSet<MessageModel> Messages { get; set; } = null!;

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