using monsters.backend.Models.Enums;

namespace monsters.backend.Models;

public class CreatureType
{
    public Guid Id { get; set; } 
    public required string Name { get; set; }
    
    public required HarvestSkill AssociatedSkill { get; set; }
    
    public ICollection<CCLink> ComponentLinks { get; set; } = new List<CCLink>();
    
}