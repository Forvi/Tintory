﻿using COLOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COLOR.Data.Configuration;

public class PaletteConfiguration : IEntityTypeConfiguration<PaletteEntity>
{
    public void Configure(EntityTypeBuilder<PaletteEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .IsRequired();
        
        builder
            .Property(p => p.Name)
            .HasMaxLength(30);

        builder.HasOne(p => p.User)
            .WithMany(u => u.Palettes)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(p => p.Colors)
            .WithMany(c => c.Palettes)
            .UsingEntity<Dictionary<string, object>>(
                "PaletteColor",
                j => j
                    .HasOne<ColorEntity>()
                    .WithMany()
                    .HasForeignKey("ColorId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<PaletteEntity>()
                    .WithMany()
                    .HasForeignKey("PaletteId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
    }
}