
using Humanizer;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel;
using WebApplication1.Model;

namespace WebApplication1.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly EmployeeDBContax _employeeDBContax;
       

        public EmployeeRepository(EmployeeDBContax employeeDBContax)
        {
            _employeeDBContax = employeeDBContax;
        }
        public async Task<int> CreateNewEmployeeAsync(Employee employee, bool useNotracking = false)
        {
           
            _employeeDBContax.employees.AddAsync(employee);
            await _employeeDBContax.SaveChangesAsync();
            return employee.EmployeeId;
        }

        public async Task<bool> DeleteEmployeeAsync(int Id, bool useNotracking = false)
        {
            var employeeID = _employeeDBContax.employees.Where(A => A.EmployeeId == Id).FirstOrDefault();
            if (employeeID != null)
            {
                _employeeDBContax.employees.Remove(employeeID);
                await _employeeDBContax.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Employee>> GetALLEmplooyeeAsync()
        {
            var employees = await _employeeDBContax.employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int Id, bool useNotracking = false)
        {
            if (useNotracking == true)
            {
                 return  await _employeeDBContax.employees.AsNoTracking().Where(a => a.EmployeeId == Id).FirstOrDefaultAsync();

            }
            else
            {
                 return await _employeeDBContax.employees.Where(a => a.EmployeeId == Id).FirstOrDefaultAsync();

            }
            
        }

        public async Task<Employee> GetEmployeeByNameAsync(string Name, bool useNotracking = false)
        {
            var employees = await _employeeDBContax.employees.Where(a => a.EmployeeName == Name).FirstOrDefaultAsync();
            return employees;
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee, bool useNotracking = false)
        {
            var existingRecord = await _employeeDBContax.employees.AsNoTracking().Where(a => a.EmployeeId == employee.EmployeeId).FirstOrDefaultAsync();
           
            
            if (existingRecord == null)
            {
                throw new Exception($"The Employee with ID {employee.EmployeeId} Not found");
            }
            else
            {
                //existingRecord.EmployeeName = employee.EmployeeName;
                //existingRecord.Email = employee.Email;
                //existingRecord.EmployeeAge = employee.EmployeeAge;
                //existingRecord.Experience = employee.Experience;
                //existingRecord.DateofJoining = employee.DateofJoining;
                //existingRecord.Department = employee.Department;
                //existingRecord.Description = employee.Description;


                _employeeDBContax.Update(employee);
                await _employeeDBContax.SaveChangesAsync();
                return existingRecord.EmployeeId;
                
            }

        }
       
    }
}
