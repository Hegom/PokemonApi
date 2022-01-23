using System.ComponentModel.DataAnnotations;

namespace PokemonApi.Core.Model
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
