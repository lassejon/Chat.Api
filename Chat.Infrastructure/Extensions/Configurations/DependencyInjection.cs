using Chat.Application.Extensions.Configuration;
using Chat.Application.Interfaces;
using Chat.Application.Interfaces.Persistence;
using Chat.Application.Services;
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

namespace Chat.Infrastructure.Extensions.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.ConfigureSettings<DatabaseSettings>();

        services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<ChatDbContext>());
        services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IRepository<Message>, MessageRepository>();
        services.AddScoped<IConversationRepository<Conversation>, ConversationRepository>();

        services.AddScoped<ILoginService, LoginService>();

        return services;
    }
}