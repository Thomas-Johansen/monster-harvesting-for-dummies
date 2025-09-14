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

        //Creature Type
        
        var aberration  = new CreatureType { Id = Guid.NewGuid(), Name = "Aberration",  AssociatedSkill = HarvestSkill.Arcana};
        var beast       = new CreatureType { Id = Guid.NewGuid(), Name = "Beast",       AssociatedSkill = HarvestSkill.Survival };
        var celestial   = new CreatureType { Id = Guid.NewGuid(), Name = "Celestial",   AssociatedSkill = HarvestSkill.Religion };
        var construct   = new CreatureType { Id = Guid.NewGuid(), Name = "Construct",   AssociatedSkill = HarvestSkill.Investigation };
        var dragon      = new CreatureType { Id = Guid.NewGuid(), Name = "Dragon",      AssociatedSkill = HarvestSkill.Survival };
        var elemental   = new CreatureType { Id = Guid.NewGuid(), Name = "Elemental",   AssociatedSkill = HarvestSkill.Arcana };
        var fey         = new CreatureType { Id = Guid.NewGuid(), Name = "Fey",         AssociatedSkill = HarvestSkill.Arcana };
        var fiend       = new CreatureType { Id = Guid.NewGuid(), Name = "Fiend",       AssociatedSkill = HarvestSkill.Religion };
        var giant       = new CreatureType { Id = Guid.NewGuid(), Name = "Giant",       AssociatedSkill = HarvestSkill.Medicine };
        var humanoid    = new CreatureType { Id = Guid.NewGuid(), Name = "Humanoid",    AssociatedSkill = HarvestSkill.Medicine };
        var monstrosity = new CreatureType { Id = Guid.NewGuid(), Name = "Monstrosity", AssociatedSkill = HarvestSkill.Survival };
        var ooze        = new CreatureType { Id = Guid.NewGuid(), Name = "Ooze",        AssociatedSkill = HarvestSkill.Nature };
        var plant       = new CreatureType { Id = Guid.NewGuid(), Name = "Plant",       AssociatedSkill = HarvestSkill.Nature };
        var undead      = new CreatureType { Id = Guid.NewGuid(), Name = "Undead",      AssociatedSkill = HarvestSkill.Medicine };
        
        db.AddRange(aberration, beast, celestial, construct, dragon, elemental, fey, fiend, giant, humanoid, monstrosity, ooze, plant, undead);
        
        
        
        //Components
        

        
        /*
        var eye = new Component { Id = Guid.NewGuid(), Name = "Eye"};
        var heart = new Component { Id = Guid.NewGuid(), Name = "Heart"};
        
        var owlbear = new CreatureType { Id = Guid.NewGuid(), Name = "Owlbear",  AssociatedSkill = HarvestSkill.Survival };
        var redDragon = new CreatureType { Id = Guid.NewGuid(), Name = "Red Dragon",  AssociatedSkill = HarvestSkill.Medicine };

        

        db.AddRange(
            eye, heart, owlbear, redDragon,
            new CCLink { CreatureType = owlbear, Component = eye, DifficultyClass = 12 },
            new CCLink { CreatureType = owlbear, Component = heart, DifficultyClass = 18 },
            new CCLink { CreatureType = redDragon, Component = eye, DifficultyClass = 13 },   
            new CCLink { CreatureType = redDragon, Component = heart, DifficultyClass = 22 }
        );
        */
        
        
        await db.SaveChangesAsync();
    }
}