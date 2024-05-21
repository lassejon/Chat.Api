using Chat.Application.Extensions.Configuration;
using Chat.Infrastructure.Extensions.Configurations;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Chat.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSignalR();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer {token}'."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Authentication
builder.Services.ConfigureIdentity();
builder.Services.AddJwtAuthentication(builder.Configuration["JWT:ValidAudience"], builder.Configuration["JWT:ValidIssuer"], builder.Configuration["JWT:Secret"] ?? "Default secret");

builder.Services
    .AddInfrastructure()
    .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true"));
}

app.MapHub<ChatHub>("/chat");
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.UseCors("ClientPermission");


app.Run();