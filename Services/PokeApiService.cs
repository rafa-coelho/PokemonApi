using Pokedex.Data.Model;
using Pokedex.Services.Interfaces;

namespace Pokedex.Services;

public class PokeApiService : IPokeApiService
{
    Task<List<PokemonModel>> IPokeApiService.GetRandomPokemonsAsync()
    {
        throw new NotImplementedException();
    }

    Task<PokemonModel> IPokeApiService.GetPokemonByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
