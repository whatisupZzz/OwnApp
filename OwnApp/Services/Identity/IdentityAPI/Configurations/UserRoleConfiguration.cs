using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityAPI.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            // 外键 User
            builder.HasOne(ur => ur.User)
                .WithMany(u=> u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            // 外键 Role
            builder.HasOne(ur => ur.Role)
                .WithMany(r=>r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();
        }
    }
}
