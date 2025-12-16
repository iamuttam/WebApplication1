using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");
            builder.HasKey(x => x.DepartmentId);
            builder.Property(x => x.DepartmentId).UseIdentityColumn();
            builder.Property(x => x.DepartmentName);
            builder.Property(n => n.Description).HasMaxLength(200);



            builder.HasData(new List<Department>()
            {
                new Department{DepartmentId = 1, DepartmentName="Uttam" ,Description="sdhfgyefywefewydfwtdfwtdfwtdrwdf"},

                new Department{DepartmentId = 2,DepartmentName="Development",Description="sdhfgyefywefewydfwtdfwtdfwtdrwdf"},

            });

        }
    }
}
