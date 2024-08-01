using Pokedex.Data.Model;

namespace Pokedex.Services.Interfaces;

public interface IPokeApiService
{
    Task<List<PokemonModel>> GetRandomPokemonsAsync();
    Task<PokemonModel> GetPokemonByIdAsync(int id);
}
