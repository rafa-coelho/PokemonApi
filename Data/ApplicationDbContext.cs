using Microsoft.EntityFrameworkCore;
using Pokedex.Data.Model;

namespace Pokedex.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<MasterPokemonModel> MasterPokemons { get; set; }
    public DbSet<PokemonModel> Pokemons { get; set; }
    public DbSet<MasterPokemonPokemon> MasterPokemonPokemons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MasterPokemonModel>()
            .HasMany(x => x.CapturedPokemons)
            .WithMany(x => x.Masters)
            .UsingEntity<MasterPokemonPokemon>(
                x => x.HasOne(x => x.PokemonModel).WithMany().HasForeignKey(x => x.PokemonModelId),
                x => x.HasOne(x => x.MasterPokemonModel).WithMany().HasForeignKey(x => x.MasterPokemonModelId),
                x =>
                {
                    x.HasKey(x => new { x.MasterPokemonModelId, x.PokemonModelId });
                });

        modelBuilder.Entity<MasterPokemonModel>()
            .HasIndex(x => x.Cpf)
            .IsUnique();
    }
}
