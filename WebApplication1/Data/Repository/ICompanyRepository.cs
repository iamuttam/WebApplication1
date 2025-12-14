using System.Linq.Expressions;

namespace WebApplication1.Data.Repository
{
    public interface ICompanyRepository<T>
    {
        Task<List<T>> GetALLEmplooyeeAsync();
        Task<T> GetEmployeeByIdAsync(Expression<Func<T, bool>> filter, bool useNotracking = false);
        Task<T> GetEmployeeByNameAsync(Expression<Func<T, bool>> filter);
        Task<T> CreateNewEmployeeAsync(T dbRecord);
        Task<T> UpdateEmployeeAsync(T dbRecord);
        Task<T> DeleteEmployeeAsync(T dbRecord);
    }
}
