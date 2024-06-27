using System.Security.Cryptography.X509Certificates;
using CashManager.Daily.Api.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CashManager.Daily.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHealthChecks();

        builder.Services.AddMongo(configuration);
        builder.Services.AddRabbitMq(configuration);
        builder.Services.AddRepository();
        builder.Services.AddServiceAppServices();
    
        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.MapHealthChecks("/api/health");
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
