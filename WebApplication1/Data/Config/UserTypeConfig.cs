using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data.Config
{
    public class UserTypeConfig : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {

            builder.ToTable("UserType");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(500);
            //Add-Migration <AddingRoleTable>
            //Update-Database
            builder.HasData(new List<UserType>()
            {
                new UserType
                {
                    Id = 1,
                    Name="Developer",
                    Description="For Developer"
                },
                 new UserType
                {
                    Id = 2,
                    Name="HR",
                    Description="For HR"
                }, new UserType
                {
                    Id = 3,
                    Name="Vice Presedent",
                    Description="For VP"
                }

            });
           
        }
    }
}
