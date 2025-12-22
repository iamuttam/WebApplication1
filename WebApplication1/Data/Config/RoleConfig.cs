using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace WebApplication1.Data.Config
{
    public class RoleConfig:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role>builder) {

            builder.ToTable("Role");
            builder.HasKey(x=>x.RoleId);
            builder.Property(x => x.RoleId).UseIdentityColumn();
            builder.Property(x=>x.RoleName).IsRequired();
            builder.Property(x=>x.Description);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();

            //Add-Migration <AddingRoleTable>
            //Update-Database
        }
    }
}
