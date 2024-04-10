using Chat.Application.Extensions.Configuration;
using Chat.Application.Interfaces;
using Chat.Domain.Conversation;
using Chat.Infrastructure.Persistence;
using Chat.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Chat.Domain.Message;
using Chat.Domain.User;
using Chat.Infrastructure.Persistence.Conversations;
using Chat.Infrastructure.Persistence.Message;
using Chat.Infrastructure.Persistence.User;

namespace Chat.Infrastructure.Extensions.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.ConfigureSettings<DatabaseSettings>();

        services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<ChatDbContext>());
        services.AddScoped<IRepository<User, UserRepository>, UserRepository>();
        services.AddScoped<IRepository<Message, MessageRepository>, MessageRepository>();
        services.AddScoped<IRepository<Conversation, ChatRepository>, ChatRepository>();

        return services;
    }
}