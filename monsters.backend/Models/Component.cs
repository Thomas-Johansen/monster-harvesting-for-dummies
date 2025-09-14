namespace monsters.backend.Models;

public class Component
{
    public Guid Id { get; set; } //Use name as id to allow easier manual editing of tables?
    public required string Name { get; set; }
    
    public ICollection<CCLink> CreatureLinks { get; set; } = new List<CCLink>();
    
    //Commented out becaused realized that there are outliers so not all components of the same type have the same DC
    //public required int DifficultyClass { get; set; }
}