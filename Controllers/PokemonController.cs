using Microsoft.AspNetCore.Mvc;
using Pokedex.Services.Interfaces;

namespace PokemonApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokeApiService _pokeApiService;

    public PokemonController(IPokeApiService pokeApiService)
    {
        _pokeApiService = pokeApiService;
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomPokemons()
    {
        var pokemons = await _pokeApiService.GetRandomPokemonsAsync();
        return Ok(pokemons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPokemonById(int id)
    {
        var pokemon = await _pokeApiService.GetPokemonByIdAsync(id);
        return Ok(pokemon);
    }
}