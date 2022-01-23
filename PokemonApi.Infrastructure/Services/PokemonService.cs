using PokemonApi.Core.Model;

namespace PokemonApi.Infrastructure
{
    public class PokemonService : IPokemonService
    {
        public IPokemonRepository Repository { get; set; }

        public PokemonService(IPokemonRepository repository)
        {
            Repository = repository;
        }

        public Task<bool> Create(int userId, Pokemon pokemon) => Repository.Create(userId, pokemon);

        public Task<List<Pokemon>> GetAll(int page, int pageSize) => Repository.GetAll(page, pageSize); 

        public Task<List<Pokemon>> GetAllByUser(int userId, int page, int pageSize) => Repository.GetAllByUser(userId, page, pageSize); 

        public Task<(bool, string)> Update(int userId, int id, Pokemon pokemon) => Repository.Update(userId, id, pokemon);
    }
}
