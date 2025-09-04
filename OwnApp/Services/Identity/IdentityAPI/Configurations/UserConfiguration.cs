
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityAPI.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");


            // Indexes
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.PhoneNumber);
            // Relationships
            builder.HasMany<RefreshToken>()
                    .WithOne(rt => rt.User)
                    .HasForeignKey(rt => rt.UserId);
        }
    }
}
