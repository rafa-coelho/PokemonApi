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

    public MasterPokemonService(ApplicationDbContext context)
    {
        _context = context;
    }

    // <summary>
    // Create a master pokemon
    // </summary>
    public async Task<MasterPokemonModel> CreateMasterPokemonAsync(CreateMasterDto masterPokemon)
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

        return result.Entity;
    }

    // <summary>
    // Capture a pokemon by master id
    // if the pokemon is not captured, it will capture the pokemon otherwise it will capture the pokemon
    // if the pokemon does not exist, it will throw an exception
    // </summary>
    public Task<List<MasterPokemonModel>> CapturePokemonsAsync(int pokemonId, int masterId)
    {
        throw new NotImplementedException();
    }

    // <summary>
    // Get captured pokemon by master id
    // if the master id does not exist, it will throw an exception
    // if the master id does not have any captured pokemon, it will return an empty list
    // </summary>
    public Task<MasterPokemonModel> GetCapturedPokemonByMasterIdAsync(int masterId)
    {
        throw new NotImplementedException();
    }

    // <summary>
    // Get a master pokemon by id
    // if the master id does not exist, it will throw an exception
    // </summary>
    public Task<MasterPokemonModel> GetMasterPokemonByIdAsync(int masterId)
    {
        throw new NotImplementedException();
    }
}
