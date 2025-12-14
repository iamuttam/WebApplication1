using Microsoft.AspNetCore.Razor.Language;
using WebApplication1.Controllers;

namespace WebApplication1.Data.Repository
{
    public interface IEmployeeRepository :ICompanyRepository<Employee>
    {
        //Task< List<Employee>> GetALLEmplooyeeAsync();
        // Task<Employee> GetEmployeeByIdAsync(int Id,bool useNotracking=false);
        // Task<Employee> GetEmployeeByNameAsync(string Name, bool useNotracking = false);
        // Task<int> CreateNewEmployeeAsync(Employee employee, bool useNotracking = false);
        // Task<int> UpdateEmployeeAsync(Employee employee, bool useNotracking = false);
        // //int UpdateEmployeePartial ();
        // Task<bool> DeleteEmployeeAsync(int Id, bool useNotracking = false);


        Task<List<Employee>> GetEmployeeByFeeStatusAsync(int Salarystatus);
    }
}
