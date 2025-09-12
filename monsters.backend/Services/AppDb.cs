

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
            e.HasIndex(x => x.Name).IsUnique(); // "Eye" should be a single canonical component
        });
            
        b.Entity<CreatureType>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasIndex(x => x.Name).IsUnique();
            e.Property(x => x.AssociatedSkill).HasConversion<string>().HasMaxLength(50);
        });
        
        b.Entity<CCLink>(e =>
        {
            // Composite PK: one row per (creature, component)
            e.HasKey(x => new { x.CreatureTypeId, x.ComponentId });

            e.Property(x => x.DifficultyClass).IsRequired();

            e.HasOne(x => x.CreatureType)
                .WithMany(ct => ct.ComponentLinks)
                .HasForeignKey(x => x.CreatureTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Component)
                .WithMany(c => c.CreatureLinks)
                .HasForeignKey(x => x.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}