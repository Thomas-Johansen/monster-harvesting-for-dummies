

using Microsoft.EntityFrameworkCore;
using monsters.backend.Models;

namespace monsters.backend.Services;

public class AppDb : DbContext
{
    public AppDb(DbContextOptions<AppDb> options) : base(options) { }
    
    public DbSet<Component> Components => Set<Component>();
    public DbSet<CreatureType> CreatureTypes => Set<CreatureType>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        
        b.Entity<Component>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.Property(x => x.DifficultyClass).IsRequired();
            e.Property(x =>x.isCraftingMaterial).IsRequired().HasDefaultValue(true);
            e.Property(x => x.isEdible).IsRequired().HasDefaultValue(false);
        });
            
        b.Entity<CreatureType>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => x.Name).IsUnique();
            e.Property(x => x.AssociatedSkill).HasConversion<string>().HasMaxLength(50);
            
            e.HasMany(x => x.Components)
                .WithOne(x => x.CreatureType)
                .HasForeignKey(x => x.CreatureTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
    }
}