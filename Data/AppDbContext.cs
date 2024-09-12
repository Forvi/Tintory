using COLOR.Data.Configuration;
using COLOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace COLOR.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<PaletteEntity> PaletteEntities => Set<PaletteEntity>();
    public DbSet<ColorEntity> ColorEntities => Set<ColorEntity>();
    public DbSet<UserEntity> UserEntities => Set<UserEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PaletteConfiguration());
        modelBuilder.ApplyConfiguration(new ColorConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}