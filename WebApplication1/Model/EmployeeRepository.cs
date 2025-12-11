using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace WebApplication1.Model
{
    public static class EmployeeRepository
    {
        public static List<Employee> employees = new List<Employee>()
        {
             new Employee{
                    EmployeeId=1001,
                    EmployeeName="Uttam",
                    Email="uttam@gmail.com",
                    EmployeeAge=25,
                    Gender="M",
                    Experience="4 yr.",
                    Department="Development",
                    Description="hI JHB"
             },
              new Employee{
                EmployeeId=1002,
                EmployeeName="Uttam1",
                Email="uttam@gmail.com",
                EmployeeAge=21,
                Gender="f",
                Experience="4 yr.",
                Department="Development",
                Description="hI JHB"
              },
              new Employee{
                EmployeeId=1003,
                EmployeeName="UttamKumar",
                Email="uttam@gmail.com",
                EmployeeAge=22,
                Gender="F",
                Experience="4 yr.",
                Department="Development",
                Description="hI JHB"
              },
              new Employee{
                EmployeeId=1004,
                EmployeeName="UttamKumar1",
                Email="uttam@gmail.com",
                EmployeeAge=23,
                Gender="M",
                Experience="4 yr.",
                Department="Development",
                Description="hI JHB"
              },
              new Employee{
                EmployeeId=1005,
                EmployeeName="Uttam Kumar2",
                Email="uttam@gmail.com",
                EmployeeAge=24,
                Gender="M",
                Experience="4 yr.",
                Department="Development",
                Description="hI JHB"
              },
              new Employee{
                EmployeeId=1006,
                EmployeeName="Uttam Kumar3",
                Email="uttam@gmail.com",
                EmployeeAge=26,
                Gender="F",
                Experience="4 yr.",
                Department="Development",
                Description="hI JHB"
              },
              new Employee{
                EmployeeId=1007,
                EmployeeName="Uttam Kumar4",
                Email="uttam@gmail.com",
                EmployeeAge=2,
                Gender="M",
                Experience="4 yr.",
                Department="Development",
                Description="hI JHB"
              },

        };
    }
}
