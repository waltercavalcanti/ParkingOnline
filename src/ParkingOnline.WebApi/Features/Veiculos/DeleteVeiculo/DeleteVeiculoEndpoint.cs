using Carter;

namespace ParkingOnline.WebApi.Features.Veiculos.DeleteVeiculo;

public class DeleteVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/veiculos/Delete/{id}", async (int id, IDeleteVeiculoHandler handler) =>
        {
            try
            {
                var foiDeletado = await handler.DeleteVeiculoAsync(id);

                if (!foiDeletado)
                {
                    return Results.NotFound($"Não há veículo cadastrado com o id {id}.");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Veiculo");
    }
}