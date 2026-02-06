using Carter;
using ParkingOnline.WebApi.Data;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Features.Clientes.CreateCliente;
using ParkingOnline.WebApi.Features.Clientes.DeleteCliente;
using ParkingOnline.WebApi.Features.Clientes.GetAllClientes;
using ParkingOnline.WebApi.Features.Clientes.GetClienteById;
using ParkingOnline.WebApi.Features.Clientes.UpdateCliente;
using ParkingOnline.WebApi.Features.Tarifas.CreateTarifa;
using ParkingOnline.WebApi.Features.Tarifas.DeleteTarifa;
using ParkingOnline.WebApi.Features.Tarifas.GetAllTarifas;
using ParkingOnline.WebApi.Features.Tarifas.GetTarifaById;
using ParkingOnline.WebApi.Features.Tarifas.UpdateTarifa;
using ParkingOnline.WebApi.Features.Vagas.CreateVaga;
using ParkingOnline.WebApi.Features.Vagas.DeleteVaga;
using ParkingOnline.WebApi.Features.Vagas.GetAllVagas;
using ParkingOnline.WebApi.Features.Vagas.GetVagaById;
using ParkingOnline.WebApi.Features.Vagas.GetVagasLivres;
using ParkingOnline.WebApi.Features.Vagas.UpdateVaga;
using ParkingOnline.WebApi.Features.Veiculos.CreateVeiculo;
using ParkingOnline.WebApi.Features.Veiculos.DeleteVeiculo;
using ParkingOnline.WebApi.Features.Veiculos.GetAllVeiculos;
using ParkingOnline.WebApi.Features.Veiculos.GetVeiculoById;
using ParkingOnline.WebApi.Features.Veiculos.UpdateVeiculo;
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

        services.AddScoped<ICreateTarifaHandler, CreateTarifaHandler>();
        services.AddScoped<IGetAllTarifasHandler, GetAllTarifasHandler>();
        services.AddScoped<IGetTarifaByIdHandler, GetTarifaByIdHandler>();
        services.AddScoped<IUpdateTarifaHandler, UpdateTarifaHandler>();
        services.AddScoped<IDeleteTarifaHandler, DeleteTarifaHandler>();

        services.AddScoped<ICreateVagaHandler, CreateVagaHandler>();
        services.AddScoped<IGetAllVagasHandler, GetAllVagasHandler>();
        services.AddScoped<IGetVagaByIdHandler, GetVagaByIdHandler>();
        services.AddScoped<IGetVagasLivresHandler, GetVagasLivresHandler>();
        services.AddScoped<IUpdateVagaHandler, UpdateVagaHandler>();
        services.AddScoped<IDeleteVagaHandler, DeleteVagaHandler>();

        services.AddScoped<ICreateVeiculoHandler, CreateVeiculoHandler>();
        services.AddScoped<IGetAllVeiculosHandler, GetAllVeiculosHandler>();
        services.AddScoped<IGetVeiculoByIdHandler, GetVeiculoByIdHandler>();
        services.AddScoped<IUpdateVeiculoHandler, UpdateVeiculoHandler>();
        services.AddScoped<IDeleteVeiculoHandler, DeleteVeiculoHandler>();

        services.AddEndpointsApiExplorer();
        services.AddOpenApi();

        services.AddCarter();

        return services;
    }
}