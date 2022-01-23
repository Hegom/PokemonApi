using System.ComponentModel.DataAnnotations;

namespace PokemonApi.Core.Model
{
    public class User
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[!#@?\]])(?=.*\d).{10,}$",
         ErrorMessage = "Password contains at least 10 characters, one lowercase letter, one uppercase letter and one of the following characters: !, @, #, ? or ].")]
        public string Password { get; set; }
    }
}
