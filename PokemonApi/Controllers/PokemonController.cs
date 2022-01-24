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

        /// <summary>
        /// Gets the list of all public pokemon that doesn't belong to any specific user
        /// </summary>
        /// <response code="200">Successfully retrieved all the pokemon</response>
        /// <response code="404">There are no public pokemon in the database</response>
        /// <remarks>Get all the pokemons</remarks>
        /// <param name="page" example="0">The page of the data set</param>
        /// <param name="pageSize" example="10">Total of records shown on the current page</param>
        // GET: api/Pokemon/0/50
        [HttpGet("{page}/{pageSize}")]
        [ProducesResponseType(typeof(Pokemon), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pokemon>> GetAllPokemon(int page, int pageSize)
        {
            var pokemon = await _service.GetAll(page, pageSize);
            if (pokemon == null) return NotFound();
            return Ok(pokemon);
        }

        /// <summary>
        /// Gets the list of pokemon that belongs to the logged user
        /// </summary>
        /// <response code="200">Successfully retrieved the pokemon</response>
        /// <response code="404">There are no pokemon in the database associated with the logged user</response>
        /// <remarks>Get all owned pokemons</remarks>
        /// <param name="page" example="0">The page of the data set</param>
        /// <param name="pageSize" example="10">Total of records shown on the current page</param>
        // GET: api/Pokemon/all/0/50
        [HttpGet]
        [Route("all/{page}/{pageSize}")]
        public async Task<ActionResult<Pokemon>> GetAllOwnedPokemon(int page, int pageSize)
        {
            var pokemon = await _service.GetAllByUser(LoggedUserId, page, pageSize);
            if (pokemon == null) return NotFound();
            return Ok(pokemon);
        }

        /// <summary>
        /// Update a pokemon that belongs to the logged user
        /// </summary>
        /// <response code="200">Successfully updated the pokemon</response>
        /// <response code="404">The Pokemon doesn't exist or the pokemon doesn't belong to the logged user</response>
        /// <remarks>Update the pokemon</remarks>
        /// <param name="id" example="5">The id of the pokemon to update</param>
        /// <param name="pokemon">Object pokemon with the information to be updated</param>
        // PUT: api/Pokemon/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPokemon(int id, Pokemon pokemon)
        {
            var result = await _service.Update(LoggedUserId, id, pokemon);
            if (result.Item1) return Ok("Successfully Updated");
            else return BadRequest(result.Item2);
        }

        /// <summary>
        /// Create a new pokemon and associate it with the logged user
        /// </summary>
        /// <response code="200">Successfully creted the pokemon</response>
        /// <response code="404">The Pokemon can't be created</response>
        /// <remarks>Create the pokemon</remarks>
        /// <param name="pokemon">Object pokemon with the information to be created</param>
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
