using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    public class Employee
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public int EmployeeAge { get; set; }
        public DateTime DateofJoining { get; set; }
        public string Gender { get; set; }
        //public string Department { get; set; }
        public string Experience { get; set; }
        public string Description { get; set; }

        public int ? DepartmentId { get; set; }
        public virtual Department ? Department {  get; set; }
    }
}
