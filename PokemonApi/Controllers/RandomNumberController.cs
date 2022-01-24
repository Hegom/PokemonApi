using Microsoft.AspNetCore.Mvc;
using PokemonApi.Infrastructure;

namespace PokemonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomNumberController : ControllerBase
    {
        private readonly IRandomNumberService _service;

        public RandomNumberController(IRandomNumberService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets a random number from an expernal api
        /// </summary>
        /// <response code="200">Successfully obtained the random number</response>
        /// <response code="404">Can't get the random number</response>
        /// <remarks>Get the random number</remarks>        
        // GET: api/RandomNumber/
        [HttpGet]
        public async Task<ActionResult<int?>> Find()
        {
            var number = await _service.Get();
            if (number == null) return NotFound();
            return Ok(number);
        }
    }
}
