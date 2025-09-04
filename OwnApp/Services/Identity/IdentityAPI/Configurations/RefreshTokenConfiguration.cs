using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityAPI.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // 表名
            builder.ToTable("RefreshTokens");


            // Token 本身
            builder.Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(200);

            // 过期时间
            builder.Property(rt => rt.ExpiresAt)
                .IsRequired();

            // 是否被吊销
            builder.Property(rt => rt.IsRevoked)
                .IsRequired()
                .HasDefaultValue(false);

            // 外键关系 (RefreshToken → User)
            builder.HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId);

            // 索引
            builder.HasIndex(rt => rt.Token).IsUnique();
            builder.HasIndex(rt => rt.UserId);
        }
    }
}
