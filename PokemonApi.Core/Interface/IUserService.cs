using PokemonApi.Core.Model;

namespace PokemonApi.Core.Interface
{
    public interface IUserService
    {
        Task<bool> Create(User user);
        Task<User> Find(string name);
        Task<bool> Update(int id, User user);
        Task<User> Get(int id);
        Task<(bool, string)> Register(User user);
        Task<AuthResponse> Authenticate(AuthRequest authRequest);
    }
}