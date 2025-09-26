using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.Dtos
{
    public class updateDepartmentDto
    {

        [Required(ErrorMessage = "Code is Required! ")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is Required! ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Create At is Required! ")]
        public DateTime CreatedAt { get; set; }
    }
}
