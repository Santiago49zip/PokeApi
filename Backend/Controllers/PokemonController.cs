using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Models.DTOs;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonService _service;

        public PokemonController(PokemonService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0,
            [FromQuery] string search = "")
        {
            List<PokemonDto> pokemons;

            if (!string.IsNullOrWhiteSpace(search))
            {
                // Buscar Pokémon por nombre
                var pokemon = await _service.GetPokemonByNameAsync(search);
                if (pokemon == null)
                    return NotFound(new { message = "No se encontró Pokémon" });

                pokemons = new List<PokemonDto> { pokemon };
            }
            else
            {
                // Listado paginado normal
                pokemons = await _service.GetPokemonsAsync(limit, offset);
            }

            return Ok(pokemons);
        }

        [HttpGet("db")]
        public async Task<IActionResult> GetFromDb()
        {
            var pokemons = await _service.GetFromDbAsync();
            return Ok(pokemons);
        }
    }
}
