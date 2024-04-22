using Chat.Application.Services;
using Chat.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Application.Extensions.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IConversationService, ConversationService>();
        services.AddScoped<IMessageService, MessageService>();

        return services;
    }
}