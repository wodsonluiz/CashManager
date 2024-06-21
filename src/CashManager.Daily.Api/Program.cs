
using System;
using System.Linq;
using System.Security.AccessControl;
using CashManager.Daily.Api.Domain.CustomerAgg;
using CashManager.Daily.Api.Infrastructure.Mongo;
using CashManager.Daily.Api.Repository;
using CashManager.Daily.Api.Repository.Customer;
using CashManager.Daily.Api.Services.Abstractions;
using CashManager.Daily.Api.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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

        builder.Services.AddMongo(configuration);
        builder.Services.AddRepository();
        builder.Services.AddServiceAppServices();

        builder.Services.AddControllers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

}

public static class ServiceExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("Mongo");
        var options = new MongoOptions();
        
        configuration.GetSection(MongoOptions.PREFIX).Bind(options);

        services.AddSingleton<IMongoProvider>(_ => 
        {
            return new MongoProvider(conn, options);
        });

        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Customer>>(sp => 
        {
            var provider = sp.GetRequiredService<IMongoProvider>();

            return new Repository<Customer>(provider, "Customers");
        });

        return services;
    }

    public static IServiceCollection AddServiceAppServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerAppService, CustomerAppServices>();

        return services;
    }
}
