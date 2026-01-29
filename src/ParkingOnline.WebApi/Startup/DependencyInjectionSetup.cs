using Carter;
using ParkingOnline.WebApi.Data;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Features.Clientes.CreateCliente;
using ParkingOnline.WebApi.Features.Clientes.DeleteCliente;
using ParkingOnline.WebApi.Features.Clientes.GetAllClientes;
using ParkingOnline.WebApi.Features.Clientes.GetClienteById;
using ParkingOnline.WebApi.Features.Clientes.UpdateCliente;
using ParkingOnline.WebApi.Shared.Data;

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
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<ITarifaRepository, TarifaRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IVagaRepository, VagaRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<ICreateClienteHandler, CreateClienteHandler>();
        services.AddScoped<IGetAllClientesHandler, GetAllClientesHandler>();
        services.AddScoped<IGetClienteByIdHandler, GetClienteByIdHandler>();
        services.AddScoped<IUpdateClienteHandler, UpdateClienteHandler>();
        services.AddScoped<IDeleteClienteHandler, DeleteClienteHandler>();

        services.AddEndpointsApiExplorer();
        services.AddOpenApi();

        services.AddCarter();

        return services;
    }
}