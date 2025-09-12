using Microsoft.EntityFrameworkCore;
using monsters.backend.Models;

namespace monsters.backend.Services;


//Testing stuffs
public class DbInitializer
{
    public static async Task SeedAsync(AppDb db)
    {
        await db.Database.MigrateAsync();

        if (await db.Components.AnyAsync()) return;

        var eye = new Component { Id = Guid.NewGuid(), Name = "Eye"};
        var heart = new Component { Id = Guid.NewGuid(), Name = "Heart"};

        var owlbear = new CreatureType { Id = Guid.NewGuid(), Name = "Owlbear", ChallengeRating = 3, AssociatedSkill = HarvestSkill.Survival };
        var redDragon = new CreatureType { Id = Guid.NewGuid(), Name = "Red Dragon", ChallengeRating = 24, AssociatedSkill = HarvestSkill.Medicine };

        db.AddRange(
            eye, heart, owlbear, redDragon,
            new CCLink { CreatureType = owlbear, Component = eye, DifficultyClass = 12 },
            new CCLink { CreatureType = owlbear, Component = heart, DifficultyClass = 18 },
            new CCLink { CreatureType = redDragon, Component = eye, DifficultyClass = 13 },   
            new CCLink { CreatureType = redDragon, Component = heart, DifficultyClass = 22 }
        );

        await db.SaveChangesAsync();
    }
}