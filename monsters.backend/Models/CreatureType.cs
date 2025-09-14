namespace monsters.backend.Models;

public class CreatureType
{
    public Guid Id { get; set; } //Use name as id to allow easier manual editing of tables?
    public required string Name { get; set; }
    
    public required HarvestSkill AssociatedSkill { get; set; }
    
    public ICollection<CCLink> ComponentLinks { get; set; } = new List<CCLink>();
    
}