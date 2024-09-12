using COLOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COLOR.Data.Configuration;

public class ColorConfiguration : IEntityTypeConfiguration<ColorEntity>
{
    public void Configure(EntityTypeBuilder<ColorEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder
            .HasMany(c => c.Palettes)
            .WithMany(p => p.Colors);
        builder
            .Property(c => c.Id)
            .IsRequired();
    }
}