using System.ComponentModel.DataAnnotations;

namespace Company.Dots
{
    public class ResetPasswordDto
    {
        //P@assw0rd
        [Required(ErrorMessage = "Password Is REquired !!")]
        [DataType(DataType.Password)] //******
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Confirm Password Is REquired !!")]
        [DataType(DataType.Password)] //******
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password dose not match the password !!")]
        public string ConfirmPassword { get; set; }
    }
}
