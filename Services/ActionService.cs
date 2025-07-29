using AssistenteLocal.Api.Data;
using AssistenteLocal.Api.Models;
using Microsoft.EntityFrameworkCore;

public class ActionService
{
    private readonly AppDbContext _db;

    public ActionService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<ActionEntity>> GetAll()
    {
        return await _db.Actions.Include(a => a.Responses).ToListAsync();
    }

    public async Task<ActionEntity?> Create(ActionEntity action)
    {
        _db.Actions.Add(action);
        await _db.SaveChangesAsync();
        return action;
    }

    public async Task<ResponseEntity?> AddResponse(int actionId, ResponseEntity response)
    {
        var action = await _db.Actions.FindAsync(actionId);
        if (action == null) return null;

        response.ActionEntityId = actionId;
        _db.Responses.Add(response);
        await _db.SaveChangesAsync();
        return response;
    }

    public async Task<string?> Execute(int id)
    {
        var action = await _db.Actions.Include(a => a.Responses)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (action == null) return null;

        return action.Responses.FirstOrDefault()?.Text ?? "Sem resposta definida";
    }
}
