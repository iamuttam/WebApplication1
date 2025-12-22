using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data.Config
{
    public class RolePrivilegeConfig :IEntityTypeConfiguration<RolePrivilege>
    {
        public void Configure(EntityTypeBuilder<RolePrivilege> builder)
        {

            builder.ToTable("RolePrivilege");
            builder.HasKey(x => x.PrivilegeId);
            builder.Property(x => x.PrivilegeId).UseIdentityColumn();
            builder.Property(x => x.RolePrivilegeName).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.RoleId).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();


            //Foreign Key
            builder.HasOne(n => n.Role)
        .WithMany(n => n.RolePrivilege)
        .HasForeignKey(n => n.RoleId)
        .HasConstraintName("Fk_RolePrivilges_Roles");
            //Add-Migration <AddingRoleTable>
            //Update-Database
        }
    }
}
