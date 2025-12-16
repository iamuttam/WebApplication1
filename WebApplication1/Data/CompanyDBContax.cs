using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Config;

namespace WebApplication1.Data
{
    public class CompanyDBContax: DbContext
    {
        public CompanyDBContax(DbContextOptions<CompanyDBContax> options) : base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());

        }
    }
}
