using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokemonApi.Core.Model;

namespace PokemonApi.Infrastructure.Respositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IServiceScope _scope;
        private readonly PokemonApiContext _databaseContext;

        public PokemonRepository(IServiceProvider serviceProvider)
        {
            _scope = serviceProvider.CreateScope();
            _databaseContext = _scope.ServiceProvider.GetRequiredService<PokemonApiContext>();
        }

        public async Task<bool> Create(int userId, Pokemon pokemon)
        {
            pokemon.Id = _databaseContext.Pokemon.Any() ? _databaseContext.Pokemon.Max(x => x.Id) + 1 : 0;
            pokemon.CreatedyByUserId = userId;
            _databaseContext.Pokemon.Add(pokemon);
            var result = await _databaseContext.SaveChangesAsync();
            return result == 1;
        }

        public async Task<List<Pokemon>> GetAll(int page, int pageSize)
        {
            _databaseContext.Database.EnsureCreated();
            return await _databaseContext.Pokemon.Where(x => x.IsPublic).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<Pokemon>> GetAllByUser(int userId, int page, int pageSize)
            => await _databaseContext.Pokemon.Where(x => x.CreatedyByUserId == userId).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        public async Task<(bool, string)> Update(int userId, int id, Pokemon pokemon)
        {
            var poke = await _databaseContext.Pokemon.FirstOrDefaultAsync(x => x.Id == id);

            if (poke == null)
            {
                return (false, $"The pokemon with Id: {id} doesn't exists");
            }

            if (poke.CreatedyByUserId != userId)
            {
                return (false, $"This pokemon doesn't belong to you");
            }

            _databaseContext.Entry(pokemon).State = EntityState.Modified;

            var result = await _databaseContext.SaveChangesAsync();

            return result == 1 ? (true, string.Empty) : (true, "Can't update the pokemon");
        }
    }
}
