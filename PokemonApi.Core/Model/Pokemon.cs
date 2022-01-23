namespace PokemonApi.Core.Model
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Evolution { get; set; }
        public int Weight { get; set; }
        public int Width { get; set; }
        public int CreatedyByUserId { get; set; }
        public bool IsPublic { get; set; }
    }
}
