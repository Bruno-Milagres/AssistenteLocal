namespace AssistenteLocal.Api.Models;

public class ActionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<ResponseEntity> Responses { get; set; } = new();
}

public class ResponseEntity
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public int ActionEntityId { get; set; }
}
