namespace PokemonApi.Core.Model
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int TokenExpirationTimeInMinutes { get; set; }
    }
}
