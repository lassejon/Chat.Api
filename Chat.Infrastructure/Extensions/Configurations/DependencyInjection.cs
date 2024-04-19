using Chat.Application.Extensions.Configuration;
using Chat.Application.Interfaces;
using Chat.Application.Interfaces.Persistence;
using Chat.Application.Services;
using Chat.Application.Services.Interfaces;
using Chat.Domain.Conversation;
using Chat.Infrastructure.Persistence;
using Chat.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Chat.Domain.Message;
using Chat.Domain.User;
using Chat.Infrastructure.Persistence.Conversation;
using Chat.Infrastructure.Persistence.Message;
using Chat.Infrastructure.Persistence.User;
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