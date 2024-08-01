using Microsoft.EntityFrameworkCore;
using Pokedex.Data.Model;

namespace Pokedex.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PokemonModel> Pokemons { get; set; }
    public DbSet<MasterPokemonModel> MasterPokemons { get; set; }
}
