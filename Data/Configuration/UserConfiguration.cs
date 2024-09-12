using COLOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COLOR.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder
            .Property(u => u.Id)
            .IsRequired();
        builder
            .Property(u => u.Email)
            .IsRequired();
        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder.HasMany(u => u.Palettes)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId); 

    }
}