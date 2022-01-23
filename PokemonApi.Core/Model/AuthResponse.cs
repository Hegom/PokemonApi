using System.ComponentModel.DataAnnotations;

namespace PokemonApi.Core.Model
{
    public class AuthResponse
    {
       
        public string Email { get; set; }        
        public string Token { get; set; }
    }
}
