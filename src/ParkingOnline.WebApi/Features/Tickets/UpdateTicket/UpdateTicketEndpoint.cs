using Carter;
using ParkingOnline.WebApi.Features.Tickets.GetTicketById;
using ParkingOnline.WebApi.Features.Vagas.UpdateVaga;

namespace ParkingOnline.WebApi.Features.Tickets.UpdateTicket;

public class UpdateTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/tickets/Update/{id}", async (int id, UpdateTicketRequest request, IUpdateTicketHandler handler, IGetTicketByIdHandler getTicketByIdHandler, IUpdateVagaHandler updateVagaHandler) =>
        {
            try
            {
                if (id != request.Id)
                {
                    return Results.BadRequest("ID da rota não corresponde ao ID da requisição.");
                }

                var response = await getTicketByIdHandler.GetTicketByIdAsync(id);

                if (response == null || response.Ticket == null)
                {
                    return Results.NotFound($"Não há ticket cadastrado com o id {id}.");
                }

                UpdateVagaRequest updateVagaRequest = new(response.Ticket.Vaga.Id, response.Ticket.Vaga.Localizacao, false);

                var foiAtualizado = await updateVagaHandler.UpdateVagaAsync(updateVagaRequest);

                if (!foiAtualizado)
                {
                    return Results.NotFound($"Não há vaga cadastrada com o id {response.Ticket.Vaga.Id}.");
                }

                foiAtualizado = await handler.UpdateTicketAsync(request);

                if (!foiAtualizado)
                {
                    return Results.NotFound($"Não há ticket cadastrado com o id {id}.");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Ticket");
    }
}