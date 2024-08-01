using Microsoft.EntityFrameworkCore;
using Pokedex.Services;
using Pokedex.Services.Interfaces;
using PokemonApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

using var db = builder
    .Services
    .BuildServiceProvider()
    .GetService<ApplicationDbContext>();
    
db.Database.Migrate();

builder.Services.AddScoped<IPokeApiService, PokeApiService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }

