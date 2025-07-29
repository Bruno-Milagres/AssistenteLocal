using AssistenteLocal.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AssistenteLocal.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<ActionEntity> Actions { get; set; }
    public DbSet<ResponseEntity> Responses { get; set; }
}
