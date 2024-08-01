using PokeApiNet;
using Pokedex.Data.Model;
using Pokedex.Services.Interfaces;

namespace Pokedex.Services;

public class PokeApiService : IPokeApiService
{
    private readonly PokeApiClient _pokeClient;
    private readonly int _maxPokemonId = 1025;
    private readonly int _pokemonLimit = 10;
    private Dictionary<int, string[]> _evolutions = new Dictionary<int, string[]>();

    public PokeApiService()
    {
        _pokeClient = new PokeApiClient();
    }
    
    // <summary>
    // Get a list 10 random pokemons
    // </summary>
    public async Task<List<PokemonModel>> GetRandomPokemonsAsync()
    {
        var random = new Random();
        var startFrom = random.Next(1, _maxPokemonId);

        var apiResponse = await _pokeClient.GetNamedResourcePageAsync<Pokemon>(_pokemonLimit, startFrom);

        var pokemons = await Task.WhenAll(apiResponse.Results.Select(async pokemon => {
            var pokemonDetails = await _pokeClient.GetResourceAsync<Pokemon>(pokemon.Name);

            return new PokemonModel
            {
                Id = pokemonDetails.Id,
                Name = pokemonDetails.Name,
                Evolutions = await GetEvolutionsAsync(pokemonDetails),
                Sprite = pokemonDetails.Sprites.FrontDefault,
            };
        }));


        return pokemons.ToList();
    }

    public async Task<PokemonModel> GetPokemonByIdAsync(int id)
    {
        var pokemonDetails = await _pokeClient.GetResourceAsync<Pokemon>(id);

        return new PokemonModel
        {
            Id = pokemonDetails.Id,
            Name = pokemonDetails.Name,
            Evolutions = await GetEvolutionsAsync(pokemonDetails),
            Sprite = pokemonDetails.Sprites.FrontDefault,
        };
    }

    private async Task<string[]> GetEvolutionsAsync(Pokemon pokemonDetails)
    {
        if (_evolutions.ContainsKey(pokemonDetails.Id))
        {
            return _evolutions[pokemonDetails.Id];
        }

        var species = await _pokeClient.GetResourceAsync(pokemonDetails.Species);
        var evolutionChain = await _pokeClient.GetResourceAsync(species.EvolutionChain);
        
        var evolutionNames = GetEvolutionChainNames(evolutionChain.Chain).SkipWhile(name => name != pokemonDetails.Name).Skip(1).ToArray();

        _evolutions.Add(pokemonDetails.Id, evolutionNames);

        return evolutionNames;
    }

    private List<string> GetEvolutionChainNames(ChainLink chain)
    {
        var evolutionNames = new List<string>();

        if (chain.Species != null)
        {
            evolutionNames.Add(chain.Species.Name);
        }

        if (chain.EvolvesTo != null && chain.EvolvesTo.Any())
        {
            foreach (var evolvesTo in chain.EvolvesTo)
            {
                evolutionNames.AddRange(GetEvolutionChainNames(evolvesTo));
            }
        }

        return evolutionNames;
    }
}
