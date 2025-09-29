using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.Dtos
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "Name is Required! ")]
        public string Name { get; set; }
        [Range(22,60)]
        public int? Age { get; set; }
        [RegularExpression(pattern: @"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
    , ErrorMessage = "Address must be like 123-street-city-country")]
        public string Address { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage="Email is not Valid")]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        [DataType(DataType.Currency)]

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date Of Create")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Department")]
        public int? DepartmentId { get; set; }
    }
}
