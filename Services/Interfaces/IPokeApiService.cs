using Pokedex.Data.Dtos;
using Pokedex.Data.Model;

namespace Pokedex.Services.Interfaces;

public interface IPokeApiService
{
    Task<List<PokemonDto>> GetRandomPokemonsAsync();
    Task<PokemonDto> GetPokemonByIdAsync(int id);
}
