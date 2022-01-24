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

        /// <summary>
        /// Gets a specific user if the email contains the search string parameter
        /// </summary>
        /// <response code="200">Successfully found the user</response>
        /// <response code="404">Can't find the user in the database</response>
        /// <remarks>Find the user</remarks>
        /// <param name="searchString" example="juan@gmail.com">The email of the user</param>        
        // GET: api/User/juan@gmail.com
        [HttpGet("{searchString}")]
        [Authorize]
        public async Task<ActionResult<User>> Find([FromRoute] string searchString)
        {
            var user = await _service.Find(searchString);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Update an specific user
        /// </summary>
        /// <response code="200">Successfully updated the user</response>
        /// <response code="404">The user doesn't exist</response>
        /// <remarks>Update the user</remarks>
        /// <param name="id" example="5">The id of the user to update</param>
        /// <param name="user">Object user with the information to be updated</param>
        // PUT: api/User/5        
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var result = await _service.Update(id, user);
            if (result) return Ok();
            else return BadRequest();
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <response code="200">Successfully creted the user</response>
        /// <response code="404">The user can't be created</response>
        /// <remarks>Create the user</remarks>
        /// <param name="user">Object User with the information to be created</param>
        // POST: api/User
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var result = await _service.Create(user);
            if (result) return Ok();
            else return BadRequest();
        }

        /// <summary>
        /// Register new login information using an email and password
        /// </summary>
        /// <response code="200">Successfully creted the login data</response>
        /// <response code="404">The login can't be created</response>
        /// <remarks>Create the login</remarks>
        /// <param name="user">Object User with the email and password</param>
        // POST: api/User/Register
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            var result = await _service.Register(user);
            if (result.Item1) return Ok("Successfully registered");
            else return BadRequest(result.Item2);
        }

        /// <summary>
        /// Login the user with a valid email and password
        /// </summary>
        /// <response code="200">Successfully login into the system, the return object contains the email of the logged user and the security token</response>
        /// <response code="404">Invalid credentials</response>
        /// <remarks>Login</remarks>
        /// <param name="authRequest">Object AuthRequest with the email and password</param>
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
