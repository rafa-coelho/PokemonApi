namespace Pokedex.Data.Model;
public class MasterPokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Cpf { get; set; }
    public List<PokemonModel> CapturedPokemons { get; set; } = new List<PokemonModel>();
}
