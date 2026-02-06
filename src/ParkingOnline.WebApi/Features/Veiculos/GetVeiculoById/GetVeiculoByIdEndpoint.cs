using Carter;

namespace ParkingOnline.WebApi.Features.Veiculos.GetVeiculoById;

public class GetVeiculoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/veiculos/GetById/{id}", async (int id, IGetVeiculoByIdHandler handler) =>
        {
            var response = await handler.GetVeiculoByIdAsync(id);

            return response.Veiculo == null
                ? Results.NotFound($"Não há veículo cadastrado com o id {id}.")
                : Results.Ok(response.Veiculo);
        }).WithTags("Veiculo").WithName("GetVeiculoById");
    }
}