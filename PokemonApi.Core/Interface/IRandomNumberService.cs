namespace PokemonApi.Infrastructure
{
    public interface IRandomNumberService
    {
        Task<int?> Get();     
    }
}
