using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data.Config
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employees");
            builder.HasKey(x=>x.EmployeeId);
            builder.Property(x => x.EmployeeId).UseIdentityColumn();
            builder.Property(n => n.EmployeeName).HasMaxLength(200);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(50);
            builder.Property(n => n.EmployeeAge).IsRequired().HasMaxLength(2);
            builder.Property(n => n.Gender).IsRequired();
            builder.Property(n => n.DateofJoining).HasMaxLength(200);
            builder.Property(n => n.Experience).IsRequired(false).HasMaxLength(50);
           // builder.Property(n => n.Department).IsRequired(false);
            builder.Property(n => n.Description).IsRequired(false).HasMaxLength(200);


            //builder.HasData(new List<Employee>()
            //{
            //    new Employee{EmployeeId = 1, EmployeeName="Uttam", EmployeeAge=29,Gender="M" ,Email="uttam@gmail.com",Department="Development",Experience="5 yrs.",DateofJoining=DateTime.Parse("12/02/2021"),Description="sdhfgyefywefewydfwtdfwtdfwtdrwdf"},

            //    new Employee{EmployeeId = 2, EmployeeName="Uttam Kumar", EmployeeAge=29,Gender="M" ,Email="uttam@gmail.com",Department="Development",Experience="5 yrs.",DateofJoining=DateTime.Parse("12/02/2021"),Description="sdhfgyefywefewydfwtdfwtdfwtdrwdf"},

            //    new Employee{EmployeeId = 3, EmployeeName="Uttam singh", EmployeeAge=29,Gender="M" ,Email="uttam@gmail.com",Department="Development",Experience="5 yrs.",DateofJoining=DateTime.Parse("12/02/2021"),Description="sdhfgyefywefewydfwtdfwtdfwtdrwdf"},
            //});

            builder.HasOne(n => n.Department)
                   .WithMany(n => n.Employees)
                   .HasForeignKey(n => n.DepartmentId)
                   .HasConstraintName("Fk_Employee_Department");
        }
    }
}
