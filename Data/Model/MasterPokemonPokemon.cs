namespace Pokedex.Data.Model;

public class MasterPokemonPokemon
{
    public int MasterPokemonModelId { get; set; }
    public int PokemonModelId { get; set; }
    public MasterPokemonModel MasterPokemonModel { get; set; } = null!;
    public PokemonModel PokemonModel { get; set; } = null!;
}
