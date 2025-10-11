using System.ComponentModel.DataAnnotations;

namespace Company.Dots
{
    public class ForgetPasswordDto 
    {
        [Required(ErrorMessage = "Email Is REquired !!")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
