using Microsoft.EntityFrameworkCore;
using Pokedex.Data.Model;

namespace PokemonApp.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PokemonModel> Pokemons { get; set; }
    public DbSet<MasterPokemon> MasterPokemons { get; set; }
}
