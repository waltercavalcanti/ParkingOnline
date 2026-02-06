using Carter;
using ParkingOnline.WebApi.Shared.Data;
using Scrutor;

namespace ParkingOnline.WebApi.Startup;

public static class DependencyInjectionSetup
{
    public static IServiceCollection RegistrarServicos(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("https://localhost:7121")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.AddControllers();
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.Scan(selector => selector.FromAssemblies(typeof(Program).Assembly)
                                          .AddClasses(filter => filter.Where(type => type.Name.EndsWith("Repository") || type.Name.EndsWith("Handler")), publicOnly: false)
                                          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                                          .AsMatchingInterface()
                                          .WithScopedLifetime());

        services.AddEndpointsApiExplorer();
        services.AddOpenApi();

        services.AddCarter();

        return services;
    }
}