using Pokedex.Data.Model;
using Pokedex.Data;
using Pokedex.Services.Interfaces;
using Pokedex.Data.Dtos;
using Microsoft.EntityFrameworkCore;
using Pokedex.AspNetCore.Exceptions;

namespace Pokedex.Services;
public class MasterPokemonService : IMasterPokemonService
{
    private readonly ApplicationDbContext _context;
    private readonly IPokeApiService _pokemonService;

    public MasterPokemonService(ApplicationDbContext context, IPokeApiService pokemonService)
    {
        _context = context;
        _pokemonService = pokemonService;
    }

    // <summary>
    // Create a master pokemon
    // </summary>
    public async Task<MasterPokemonDto> CreateMasterPokemonAsync(MasterPokemonDto masterPokemon)
    {
        var cpfExists = await _context.MasterPokemons.AnyAsync(x => x.Cpf == masterPokemon.Cpf);

        if (cpfExists)
            throw ApiException.BadRequest("Cpf is already in use");

        var result = await _context.MasterPokemons.AddAsync(new MasterPokemonModel
        {
            Name = masterPokemon.Name,
            Age = masterPokemon.Age,
            Cpf = masterPokemon.Cpf
        });

        await _context.SaveChangesAsync();

        return new MasterPokemonDto(result.Entity.Name, result.Entity.Age, result.Entity.Cpf, result.Entity.Id);
    }

    // <summary>
    // Get a master pokemon by id
    // if the master id does not exist, it will throw an exception
    // </summary>
    public async Task<MasterPokemonDto> GetMasterPokemonByIdAsync(int masterId)
    {
        var masterPokemon = await _context.MasterPokemons.FindAsync(masterId);

        if (masterPokemon == null)
            throw ApiException.NotFound("Master pokemon not found");

        return new MasterPokemonDto(masterPokemon.Name, masterPokemon.Age, masterPokemon.Cpf);
    }

    // <summary>
    // Capture a pokemon by master id
    // if the pokemon is not captured, it will capture the pokemon otherwise it will throw an exception
    // if the pokemon does not exist, it will throw an exception
    // </summary>
    public async Task<PokemonDto> CapturePokemonsAsync(int pokemonId, int masterId)
    {
        var masterPokemon = await _context.MasterPokemons.Include(x => x.CapturedPokemons).FirstOrDefaultAsync(x => x.Id == masterId);

        if (masterPokemon == null)
            throw ApiException.NotFound("Master pokemon not found");

        var pokemon = await _pokemonService.GetPokemonByIdAsync(pokemonId);

        var capturedPokemon = masterPokemon.CapturedPokemons.FirstOrDefault(x => x.Id == pokemonId);

        if (capturedPokemon != null)
            throw ApiException.BadRequest("Pokemon already captured");

        var pokemonEntity = await GetPokemonEntityAsync(pokemon);

        masterPokemon.CapturedPokemons.Add(pokemonEntity);

        await _context.SaveChangesAsync();

        return new PokemonDto(pokemon.Id, pokemon.Name, pokemon.Evolutions, pokemon.Sprite);
    }

    // <summary>
    // Get captured pokemon by master id
    // if the master id does not exist, it will throw an exception
    // if the master id does not have any captured pokemon, it will return an empty list
    // </summary>
    public Task<List<PokemonDto>> GetCapturedPokemonByMasterIdAsync(int masterId)
    {
        throw new NotImplementedException();
    }

    private async Task<PokemonModel> GetPokemonEntityAsync(PokemonModel pokemon)
    {
        var existingPokemon = await _context.Pokemons.FirstOrDefaultAsync(x => x.Id == pokemon.Id);
        
        if (existingPokemon != null)
            return existingPokemon;

        var pokemonEntity = new PokemonModel
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Evolutions = pokemon.Evolutions,
            Sprite = pokemon.Sprite
        };

        await _context.Pokemons.AddAsync(pokemonEntity);

        return pokemonEntity;
    }
}
