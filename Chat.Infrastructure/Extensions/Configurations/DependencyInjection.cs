using System.Text;
using Chat.Application.Extensions.Configuration;
using Chat.Application.Interfaces;
using Chat.Application.Interfaces.Persistence;
using Chat.Application.Services;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Infrastructure.Extensions.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.ConfigureSettings<DatabaseSettings>();

        services.AddScoped<IUnitOfWork>(s => s.GetRequiredService<ChatDbContext>());
        services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IRepository<Message>, MessageRepository>();
        services.AddScoped<IRepository<Conversation>, ConversationRepository>();

        services.AddScoped<ILoginService, LoginService>();

        return services;
    }
    
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string? audience, string? issuer, string secret)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;

                var validAudiences = audience?.Split(";") ?? Array.Empty<string>();
                var validIssuers = issuer?.Split(";") ?? Array.Empty<string>();
        
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudiences = validAudiences,
                    ValidIssuers = validIssuers,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
            });

        var clientPermission = "ClientPermission";

        services.AddCors(options =>
        {
            options.AddPolicy(clientPermission, policy =>
            {
                policy.WithOrigins("https://localhost:3000")
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ChatDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}