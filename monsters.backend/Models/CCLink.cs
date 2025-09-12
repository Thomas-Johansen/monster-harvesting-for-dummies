namespace monsters.backend.Models;

// Join entity: holds per-creature DC
public class CCLink
{
    public Guid CreatureTypeId { get; set; }
    public Guid ComponentId { get; set; }

    public int DifficultyClass { get; set; }        // required override per creature+component

    public CreatureType CreatureType { get; set; } = default!;
    public Component Component { get; set; } = default!;
}