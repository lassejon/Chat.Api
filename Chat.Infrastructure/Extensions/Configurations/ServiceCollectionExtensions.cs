using Chat.Domain.Users;
using Chat.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Chat.Infrastructure.Extensions.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string? audience, string? issuer, string secret)
        {

            var validAudiences = audience?.Split(";") ?? Array.Empty<string>();
            var validIssuers = issuer?.Split(";") ?? Array.Empty<string>();

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
}
