using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonApi.Core.Interface;
using PokemonApi.Core.Model;
using PokemonApi.Infrastructure;

namespace PokemonApi.Controllers
{
    [Route("api/[controller]")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }        

        // GET: api/User/juan@gmail.com
        [HttpGet("{searchString}")]
        [Authorize]
        public async Task<ActionResult<User>> Find([FromRoute] string searchString)
        {
            var user = await _service.Find(searchString);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // PUT: api/User/5        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var result = await _service.Update(id, user);
            if (result) return Ok();
            else return BadRequest();
        }

        // POST: api/User
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var result = await _service.Create(user);
            if (result) return Ok();
            else return BadRequest();
        }

        // POST: api/User/Register
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            var result = await _service.Register(user);
            if (result.Item1) return Ok("Successfully registered");
            else return BadRequest(result.Item2);
        }

        // POST: api/User/Login
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthResponse>> LoginUser([FromBody] AuthRequest authRequest)
        {           
            var result = await _service.Authenticate(authRequest);
            if (result != null) return Ok(result);
            else return BadRequest("Invalid credentials");
        }

    }
}
