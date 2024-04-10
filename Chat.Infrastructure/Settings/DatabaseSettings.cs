using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chat.Application.Settings;
using Chat.Infrastructure.Persistence;

namespace Chat.Infrastructure.Settings;

public abstract class DatabaseSettings : Settings<DatabaseSettings>
{
    private string ConnectionString { get; set; } = default!;

    public override IServiceCollection OnConfigure(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services = base.OnConfigure(services);
        
        ConnectionString = configuration[$"{SectionName}:{nameof(ConnectionString)}"]!;
        
        services.AddDbContext<ChatDbContext>(options =>
            options.UseSqlServer(ConnectionString));

        return services;
    }
}