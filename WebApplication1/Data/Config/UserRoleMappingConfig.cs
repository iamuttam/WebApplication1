using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data.Config
{
    public class UserRoleMappingConfig : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {

            builder.ToTable("UserRoleMapping");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasIndex(x =>new {x.RoleId,x.UserId},"UK_UserRoleMapping").IsUnique();
            builder.Property(x=>x.RoleId).IsRequired();
            builder.Property(xx=>xx.UserId).IsRequired();

            //Foreign Key
            builder.HasOne(n => n.Role)
        .WithMany(n => n.UserRoleMappings)
        .HasForeignKey(n => n.RoleId)
        .HasConstraintName("Fk_UserRoleMapping_Role");
            //Foreign Key
            builder.HasOne(n => n.User)
        .WithMany(n => n.UserRoleMappings)
        .HasForeignKey(n => n.UserId)
        .HasConstraintName("Fk_UserRoleMapping_User");

            //Add-Migration <AddingRoleTable>
            //Update-Database
        }
    }
}
