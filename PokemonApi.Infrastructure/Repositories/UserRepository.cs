using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PokemonApi.Core.Model;
using PokemonApi.Infrastructure.Util;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokemonApi.Infrastructure.Respositories
{
    public class UserRepository : IPokemonRepository
    {
        private readonly IServiceScope _scope;
        private readonly PokemonApiContext _databaseContext;
        private readonly AppSettings _appSettings;

        public UserRepository(IServiceProvider serviceProvider, IOptions<AppSettings> appSettings)
        {
            _scope = serviceProvider.CreateScope();
            _databaseContext = _scope.ServiceProvider.GetRequiredService<PokemonApiContext>();
            _appSettings = appSettings.Value;
        }

        public async Task<bool> Create(User user)
        {
            _databaseContext.User.Add(user);
            var result = await _databaseContext.SaveChangesAsync();
            return result == 1;
        }

        public async Task<User> Find(string email) => await _databaseContext.User.FirstOrDefaultAsync(x => x.Email.Contains(email));

        public async Task<User> Get(int id) => await _databaseContext.User.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Update(int id, User user)
        {
            if (id != user.Id)
            {
                return false;
            }

            _databaseContext.Entry(user).State = EntityState.Modified;

            var result = await _databaseContext.SaveChangesAsync();

            return result == 1;
        }

        public async Task<(bool, string)> Register(User user)
        {
            var existingUser = await _databaseContext.User.FirstOrDefaultAsync(x => x.Email == user.Email);

            if (existingUser != null)
            {
                return (false, $"The user with the email: {user.Email} already exists.");
            }

            user.Password = user.Password.GetSHA256();
            _databaseContext.User.Add(user);
            var result = await _databaseContext.SaveChangesAsync();

            return result == 1 ? (true, string.Empty) : (true, "Can't register the user");
        }

        public async Task<AuthResponse?> Authenticate(AuthRequest authRequest)
        {
            var pass = authRequest.Password.GetSHA256();
            var user = await _databaseContext.User.FirstOrDefaultAsync(x => x.Email == authRequest.Email && x.Password == pass);
            return user != null ? new AuthResponse { Email = user.Email, Token = GetToken(user) } : null;
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenExpirationTimeInMinutes),                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
