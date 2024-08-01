using Pokedex.Data.Model;

namespace Pokedex.Services.Interfaces;
public interface IMasterPokemonService
{
    Task<MasterPokemonModel> CreateMasterPokemonAsync(MasterPokemonModel masterPokemon);
    Task<MasterPokemonModel> GetMasterPokemonByIdAsync(int masterId);
    Task<List<MasterPokemonModel>> CapturePokemonsAsync(int pokemonId, int masterId);
    Task<MasterPokemonModel> GetCapturedPokemonByMasterIdAsync(int masterId);
}
