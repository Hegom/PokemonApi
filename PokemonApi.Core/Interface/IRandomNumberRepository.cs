namespace PokemonApi.Infrastructure
{
    public interface IRandomNumberRepository
    {
        Task<int?> Get();     
    }
}
