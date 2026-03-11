using Carter;
using ParkingOnline.WebApi.Domain.Tickets;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Features.Tickets.GetTicketById;
using ParkingOnline.WebApi.Features.Vagas.UpdateVaga;
using ParkingOnline.WebApi.Shared;

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
                    return Results.BadRequest(TicketErrors.IdDiscrepancy().Description);
                }

                var response = await getTicketByIdHandler.GetTicketByIdAsync(id);

                if (response == null || response.Ticket == null)
                {
                    return Results.NotFound(TicketErrors.NotFound(id).Description);
                }

                UpdateVagaRequest updateVagaRequest = new(response.Ticket.Vaga.Id, response.Ticket.Vaga.Localizacao, false);

                var foiAtualizado = await updateVagaHandler.UpdateVagaAsync(updateVagaRequest);

                if (!foiAtualizado)
                {
                    return Results.NotFound(VagaErrors.NotFound(response.Ticket.Vaga.Id).Description);
                }

                foiAtualizado = await handler.UpdateTicketAsync(request);

                if (!foiAtualizado)
                {
                    return Results.NotFound(TicketErrors.NotFound(id).Description);
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags(Tags.Ticket);
    }
}