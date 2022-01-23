using PokemonApi.Core.Interface;
using PokemonApi.Core.Model;

namespace PokemonApi.Infrastructure
{
    public class UserService : IUserService
    {
        public IPokemonRepository Repository { get; set; }

        public UserService(IPokemonRepository repository)
        {
            Repository = repository;
        }

        public async Task<bool> Create(User user) => await Repository.Create(user);

        public async Task<User> Find(string name) => await Repository.Find(name);

        public async Task<User> Get(int id) => await Repository.Get(id);

        public async Task<bool> Update(int id, User user) => await Repository.Update(id, user);

        public async Task<(bool, string)> Register(User user) => await Repository.Register(user);

        public async Task<AuthResponse> Authenticate(AuthRequest authRequest) => await Repository.Authenticate(authRequest);
    }
}
