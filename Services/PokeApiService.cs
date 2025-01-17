using PokeApiNet;
using Pokedex.AspNetCore.Exceptions;
using Pokedex.Data.Dtos;
using Pokedex.Services.Interfaces;

namespace Pokedex.Services;

public class PokeApiService : IPokeApiService
{
    private readonly PokeApiClient _pokeClient;
    private readonly int _maxPokemonId = 1025;
    private readonly int _pokemonLimit = 10;

    public PokeApiService()
    {
        _pokeClient = new PokeApiClient();
    }

    // <summary>
    // Get a list 10 random pokemons
    // </summary>
    public async Task<List<PokemonDto>> GetRandomPokemonsAsync()
    {
        var random = new Random();
        var startFrom = random.Next(1, _maxPokemonId);

        var apiResponse = await _pokeClient.GetNamedResourcePageAsync<Pokemon>(_pokemonLimit, startFrom);

        var pokemons = await Task.WhenAll(apiResponse.Results.Select(async pokemon =>
        {
            var pokemonDetails = await _pokeClient.GetResourceAsync<Pokemon>(pokemon.Name);

            var evolutionsTask = GetEvolutionsAsync(pokemonDetails);
            var spriteTask = GetBase64StringFromImageUrlAsync(pokemonDetails.Sprites.FrontDefault);

            await Task.WhenAll(evolutionsTask, spriteTask);

            return new PokemonDto(pokemonDetails.Id,
                pokemonDetails.Name,
                await evolutionsTask,
                await spriteTask);
        }));


        return pokemons.ToList();
    }

    // <summary>
    // Get a pokemon by id
    // </summary>
    public async Task<PokemonDto> GetPokemonByIdAsync(int id)
    {
        try
        {
            var pokemonDetails = await _pokeClient.GetResourceAsync<Pokemon>(id);

            var evolutionsTask = GetEvolutionsAsync(pokemonDetails);
            var spriteTask = GetBase64StringFromImageUrlAsync(pokemonDetails.Sprites.FrontDefault);

            await Task.WhenAll(evolutionsTask, spriteTask);

            return new PokemonDto(pokemonDetails.Id,
                pokemonDetails.Name,
                await evolutionsTask,
                await spriteTask);

        }
        catch
        {
            throw ApiException.NotFound("Pokemon not found");
        }

    }

    // <summary>
    // Get evolutions of a pokemon.
    // It will only return the names of next the evolutions
    // </summary>
    private async Task<string[]> GetEvolutionsAsync(Pokemon pokemonDetails)
    {
        var species = await _pokeClient.GetResourceAsync(pokemonDetails.Species);
        var evolutionChain = await _pokeClient.GetResourceAsync(species.EvolutionChain);

        return GetEvolutionChainNames(evolutionChain.Chain).SkipWhile(name => name != pokemonDetails.Name).Skip(1).ToArray();
    }

    // <summary>
    // Get all the evolution chain names
    // </summary>
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


    // <summary>
    // Get a pokemon sprite in base64 format
    // </summary>
    private static async Task<string> GetBase64StringFromImageUrlAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var bytes = await response.Content.ReadAsByteArrayAsync();
                return Convert.ToBase64String(bytes);
            }
        }
        return null;
    }
}
