using Chat.Application.Extensions.Configuration;
using Chat.Application.Interfaces;
using Chat.Application.Interfaces.Persistence;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Conversations;
using Chat.Infrastructure.Persistence;
using Chat.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Chat.Domain.Messages;
using Chat.Domain.Users;
using Chat.Infrastructure.Persistence.Conversations;
using Chat.Infrastructure.Persistence.Messages;
using Chat.Infrastructure.Persistence.Users;
using Chat.Infrastructure.Services;
using Chat.Application.Responses;

namespace Chat.Infrastructure.Extensions.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.ConfigureSettings<DatabaseSettings>();

        services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<ChatDbContext>());
        services.AddScoped<IEntityRepository<User>, UserRepository>();
        services.AddScoped<IEntityRepository<Message>, MessageRepository>();
        services.AddScoped<IConversationRepository<Conversation, ConversationsResponse>, ConversationRepository>();

        services.AddScoped<ILoginService, LoginService>();

        return services;
    }
}