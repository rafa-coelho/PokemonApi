using Microsoft.AspNetCore.Mvc;
using Pokedex.AspNetCore.Exceptions;
using Pokedex.Data.Dtos;
using Pokedex.Data.Model;
using Pokedex.Services.Interfaces;

namespace Pokedex.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MasterPokemonController : ControllerBase
{
    private IMasterPokemonService _service;

    public MasterPokemonController(IMasterPokemonService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMasterPokemon(MasterPokemonDto masterPokemon)
    {
        try
        {
            var createdMasterPokemon = await _service.CreateMasterPokemonAsync(masterPokemon);
            
            return Ok(createdMasterPokemon);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, new { Message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMasterPokemonById(int id)
    {
        try
        {
            var masterPokemon = await _service.GetMasterPokemonByIdAsync(id);
            
            return Ok(masterPokemon);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, new { Message = ex.Message });
        }
    }

    [HttpPost("capture")]
    public async Task<IActionResult> CapturePokemon([FromBody] CapturePokemonDto capturePokemonDto)
    {
        try
        {
            var capturedPokemon = await _service.CapturePokemonsAsync(capturePokemonDto.PokemonId, capturePokemonDto.MasterId);
            
            return Ok(capturedPokemon);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, new { Message = ex.Message });
        }
    }

    [HttpGet("{masterId}/captured")]
    public async Task<IActionResult> GetCapturedPokemons(int masterId)
    {
        try
        {
            var capturedPokemon = await _service.GetCapturedPokemonByMasterIdAsync(masterId);
            
            return Ok(capturedPokemon);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, new { Message = ex.Message });
        }
    }

}
