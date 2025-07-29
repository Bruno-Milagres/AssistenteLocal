using AssistenteLocal.Api.Models;
using Microsoft.EntityFrameworkCore;

public static class ActionsEndpoints
{
    public static void MapActionsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/acoes");

        // GET /acoes
        group.MapGet("/", async (ActionService service) =>
            Results.Ok(await service.GetAll()));

        // POST /acoes
        group.MapPost("/", async (ActionService service, ActionEntity action) =>
        {
            var created = await service.Create(action);
            return Results.Created($"/acoes/{created!.Id}", created);
        });

        // POST /acoes/{id}/respostas
        group.MapPost("/{id}/respostas", async (ActionService service, int id, ResponseEntity response) =>
        {
            var created = await service.AddResponse(id, response);
            return created == null
                ? Results.NotFound()
                : Results.Created($"/acoes/{id}/respostas/{created.Id}", created);
        });

        // POST /acoes/executar/{id}
        group.MapPost("/executar/{id}", async (ActionService service, int id) =>
        {
            var resposta = await service.Execute(id);
            return resposta == null
                ? Results.NotFound()
                : Results.Ok(new { Id = id, Resposta = resposta });
        });
    }
}
