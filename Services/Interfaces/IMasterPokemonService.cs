using Pokedex.Data.Dtos;
using Pokedex.Data.Model;

namespace Pokedex.Services.Interfaces;
public interface IMasterPokemonService
{
    Task<MasterPokemonDto> CreateMasterPokemonAsync(MasterPokemonDto masterPokemon);
    Task<MasterPokemonDto> GetMasterPokemonByIdAsync(int masterId);
    Task<List<PokemonDto>> GetCapturedPokemonByMasterIdAsync (int masterId);
    Task<PokemonDto> CapturePokemonsAsync (int pokemonId, int masterId);
}
