//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Validators;


namespace WebApplication1.Model
{
    public class EmployeeDTO
    {
        //[Required(ErrorMessage ="Please Enter Valid ID.")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Please Enter Valid Employee Name.")]
        public string EmployeeName { get; set; }        
        [EmailAddress(ErrorMessage = "Please Enter Valid Email Id.")]
        public string Email { get; set; }
        [Required (ErrorMessage = "Please Enter Employee Age.")]
        //[Range(20,30)]
        [AgeCheck]
        public int EmployeeAge { get; set; }
        [Required(ErrorMessage = "Please Enter Gender.")]
        public string Gender { get; set; }
        //[Required(ErrorMessage = "Please Enter Employee Department.")]
        //public string Department { get; set; }
        [Required(ErrorMessage = "Please Enter Employee Experience.")]
        public string Experience { get; set; }        
        public string Description { get; set; }
    }
}
