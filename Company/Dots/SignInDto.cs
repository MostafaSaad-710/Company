using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Company.Dots
{
    public class SignInDto
    {

        [Required(ErrorMessage = "Email Is REquired !!")]
        [EmailAddress]
        public string Email { get; set; }


        //P@assw0rd
        [Required(ErrorMessage = "Password Is REquired !!")]
        [DataType(DataType.Password)] //******
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
