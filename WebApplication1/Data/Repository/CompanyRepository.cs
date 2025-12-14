
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace WebApplication1.Data.Repository
{
    public class CompanyRepository<T> : ICompanyRepository<T> where T : class
    {
        private readonly CompanyDBContax _companyDbContax;
        private DbSet<T> _dbSet;
        public CompanyRepository(CompanyDBContax companyDbContax)
        {
            _companyDbContax = companyDbContax;
            _dbSet = _companyDbContax.Set<T>();
        }

        public async Task<T> CreateNewEmployeeAsync( T dbRecord)
        {

            _dbSet.AddAsync(dbRecord);
            await _companyDbContax.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<T> DeleteEmployeeAsync(T dbRecord)
        {
                _dbSet.Remove(dbRecord);
                await _companyDbContax.SaveChangesAsync();
                return dbRecord;
            
        }

        public async Task<List<T>> GetALLEmplooyeeAsync()
        {
           return await _dbSet.ToListAsync();
            
        }

        public async Task<T> GetEmployeeByIdAsync(Expression<Func<T,bool>> filter, bool useNotracking = false)
        {
            if (useNotracking == true)
            {
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            }
            else
            {
                return await _dbSet.Where(filter).FirstOrDefaultAsync();

            }

        }

        public async Task<T> GetEmployeeByNameAsync(Expression<Func<T, bool>> filter)
        {
            return  await _dbSet.Where(filter).FirstOrDefaultAsync();
          
        }

        public async Task<T> UpdateEmployeeAsync(T dbRecord)
        {
               _companyDbContax.Update(dbRecord);
                await _companyDbContax.SaveChangesAsync();
                return dbRecord;
        }
    }
}
