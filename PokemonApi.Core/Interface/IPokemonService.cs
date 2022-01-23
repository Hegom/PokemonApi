using PokemonApi.Core.Model;

namespace PokemonApi.Infrastructure
{
    public interface IPokemonService
    {
        Task<bool> Create(int userId, Pokemon pokemon);        
        Task<List<Pokemon>> GetAll(int page, int pageSize);
        Task<List<Pokemon>> GetAllByUser(int userId, int page, int pageSize);
        Task<(bool, string)> Update(int userId, int id, Pokemon pokemon);
    }
}
