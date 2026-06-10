using Carter;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Features.Vagas.GetVagaById;
using ParkingOnline.WebApi.Features.Vagas.UpdateVaga;
using ParkingOnline.WebApi.Features.Veiculos.GetVeiculoById;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Tickets.CreateTicket;

public class CreateTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tickets/Add", async (CreateTicketRequest request,
                                               ICreateTicketHandler handler,
                                               IGetVeiculoByIdHandler getVeiculoByIdHandler,
                                               IGetVagaByIdHandler getVagaByIdHandler,
                                               IUpdateVagaHandler updateVagaHandler) =>
        {
            GetVeiculoByIdResponse getVeiculoByIdResponse = await getVeiculoByIdHandler.GetVeiculoByIdAsync(request.VeiculoId);

            if (getVeiculoByIdResponse is null || getVeiculoByIdResponse.Veiculo is null)
            {
                return Results.NotFound(VeiculoErrors.NotFound(request.VeiculoId).Description);
            }

            GetVagaByIdResponse getVagaByIdResponse = await getVagaByIdHandler.GetVagaByIdAsync(request.VagaId);

            if (getVagaByIdResponse is null || getVagaByIdResponse.Vaga is null)
            {
                return Results.NotFound(VagaErrors.NotFound(request.VagaId).Description);
            }

            if (getVagaByIdResponse.Vaga.Ocupada)
            {
                return Results.BadRequest(VagaErrors.Ocupada(getVagaByIdResponse.Vaga.Id).Description);
            }

            await updateVagaHandler.UpdateVagaAsync(new UpdateVagaRequest(getVagaByIdResponse.Vaga.Id, getVagaByIdResponse.Vaga.Localizacao, true));

            CreateTicketResponse response = await handler.AddTicketAsync(request);

            return Results.CreatedAtRoute("GetTicketById", new { id = response.Id }, response);
        }).WithTags(Tags.Ticket);
    }
}