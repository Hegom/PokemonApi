using PokemonApi.Core.Model;

namespace PokemonApi.Infrastructure
{
    public interface IPokemonRepository
    {
        Task<bool> Create(int userId, Pokemon pokemon);        
        Task<IEnumerable<Pokemon>> GetAll(int page, int pageSize);
        Task<IEnumerable<Pokemon>> GetAllByUser(int userId, int page, int pageSize);
        Task<(bool, string)> Update(int userId, int id, Pokemon pokemon);
    }
}
