using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Config;

namespace WebApplication1.Data
{
    public class EmployeeDBContax:DbContext
    {
        public EmployeeDBContax(DbContextOptions<EmployeeDBContax> options): base(options) 
        {
            
        }
        DbSet<Employee> employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Employee>().HasData(new List<Employee>()
            //{
            //    new Employee{EmployeeId = 1, EmployeeName="Uttam", EmployeeAge=29,Gender="M" ,Email="uttam@gmail.com",Department="Development",Experience="5 yrs.",DateofJoining=DateTime.Parse("12/02/2021"),Description="sdhfgyefywefewydfwtdfwtdfwtdrwdf"},

            //    new Employee{EmployeeId = 2, EmployeeName="Uttam Kumar", EmployeeAge=29,Gender="M" ,Email="uttam@gmail.com",Department="Development",Experience="5 yrs.",DateofJoining=DateTime.Parse("12/02/2021"),Description="sdhfgyefywefewydfwtdfwtdfwtdrwdf"},

            //    new Employee{EmployeeId = 3, EmployeeName="Uttam singh", EmployeeAge=29,Gender="M" ,Email="uttam@gmail.com",Department="Development",Experience="5 yrs.",DateofJoining=DateTime.Parse("12/02/2021"),Description="sdhfgyefywefewydfwtdfwtdfwtdrwdf"},
            //});

            //modelBuilder.Entity<Employee>(entity =>
            //{
            //    entity.Property(n => n.EmployeeId).IsRequired();
            //    entity.Property(n => n.EmployeeName).HasMaxLength(200);
            //    entity.Property(n => n.Email).IsRequired().HasMaxLength(50);
            //    entity.Property(n=>n.EmployeeAge).IsRequired().HasMaxLength(2);
            //    entity.Property(n => n.Gender).IsRequired();
            //    entity.Property(n => n.DateofJoining).HasMaxLength(200);
            //    entity.Property(n => n.Experience).IsRequired(false).HasMaxLength(50);
            //    entity.Property(n => n.Department).IsRequired(false);
            //    entity.Property(n => n.Description).IsRequired(false).HasMaxLength(200);
               
            //});


            modelBuilder.ApplyConfiguration(new EmployeeConfig());

            // modelBuilder.ApplyConfiguration(new TableConfig());
            //modelBuilder.ApplyConfiguration(new TableConfig());
            //modelBuilder.ApplyConfiguration(new TableConfig());
        }
    }
}
