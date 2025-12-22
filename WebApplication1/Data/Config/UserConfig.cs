using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace WebApplication1.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(X => X.UserId);
            builder.Property(x => x.UserId).UseIdentityColumn();
            builder.Property(x => x.Username).IsRequired().HasMaxLength(128);
            builder.Property(x => x.password).IsRequired();
            builder.Property(x => x.passwordSalt).IsRequired();
            builder.Property(x => x.UserTypeId).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDelete).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedDate);
            //Add-Migration <Migration Name>
            // Update-Database 
            // Remove-Migration


            builder.HasOne(n => n.UserTypes)
                .WithMany(n => n.User)
                .HasForeignKey(n => n.UserTypeId)
                .HasConstraintName("Fk_Users_UserTypes");
        }
    }
}



