using AssistenteLocal.Api.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var dbPath = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dbPath);
var connectionString = $"Data Source={Path.Combine(dbPath, "assistente.db")}";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<ActionService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapActionsEndpoints();
app.MapGet("/", () => Results.Redirect("/scalar/v1"));
app.Run();
                                                                                                                                                            