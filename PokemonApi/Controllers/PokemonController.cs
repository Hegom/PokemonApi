using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonApi.Core.Model;
using PokemonApi.Infrastructure;
using System.Security.Claims;

namespace PokemonApi.Controllers
{
    [Route("api/[controller]")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    [ApiController]
    [Authorize]
    public class PokemonController : Controller
    {
        private readonly IPokemonService _service;

        public PokemonController(IPokemonService service)
        {
            _service = service;
        }

        // GET: api/Pokemon/0/50
        [HttpGet("{page}/{pageSize}")]
        public async Task<ActionResult<Pokemon>> GetAllPokemon(int page, int pageSize)
        {
            var pokemon = await _service.GetAll(page, pageSize);
            if (pokemon == null) return NotFound();
            return Ok(pokemon);
        }

        // GET: api/Pokemon/all/0/50
        [HttpGet]
        [Route("all/{page}/{pageSize}")]
        public async Task<ActionResult<Pokemon>> GetAllOwnedPokemon(int page, int pageSize)
        {
            var pokemon = await _service.GetAllByUser(LoggedUserId, page, pageSize);
            if (pokemon == null) return NotFound();
            return Ok(pokemon);
        }

        // PUT: api/Pokemon/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokemon(int id, Pokemon pokemon)
        {
            var result = await _service.Update(LoggedUserId, id, pokemon);
            if (result.Item1) return Ok("Successfully Updated");
            else return BadRequest(result.Item2);
        }

        // POST: api/Pokemon
        [HttpPost]
        public async Task<ActionResult<Pokemon>> PostPokemon(Pokemon pokemon)
        {
            var result = await _service.Create(LoggedUserId, pokemon);
            if (result) return Ok();
            else return BadRequest();
        }

        private int LoggedUserId  => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value); 
    }
}
