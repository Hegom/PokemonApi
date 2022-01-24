namespace PokemonApi.Infrastructure
{
    public class RandomNumberService : IRandomNumberService
    {
        public IRandomNumberRepository Repository { get; set; }

        public RandomNumberService(IRandomNumberRepository repository)
        {
            Repository = repository;
        }

        public Task<int?> Get() => Repository.Get();       
    }
}
