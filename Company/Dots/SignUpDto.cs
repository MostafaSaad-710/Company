using System.ComponentModel.DataAnnotations;

namespace Company.Dots
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "UserName Is REquired !!")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "FirstName Is REquired !!")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName Is REquired !!")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email Is REquired !!")]
        [EmailAddress]
        public string Email { get; set; }


        //P@assw0rd
        [Required(ErrorMessage = "Password Is REquired !!")]
        [DataType(DataType.Password)] //******
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password Is REquired !!")]
        [DataType(DataType.Password)] //******
        [Compare(nameof(Password) , ErrorMessage = "Confirm Password dose not match the password !!")]
        public string ConfirmPassword { get; set; }
         
        public bool IsAgree { get; set; }

    }
}
