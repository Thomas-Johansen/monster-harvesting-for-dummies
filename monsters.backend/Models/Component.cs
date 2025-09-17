using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace monsters.backend.Models;

public class Component
{
    public Guid Id { get; set; } 
    
    public Guid CreatureTypeId { get; set; }
    public CreatureType CreatureType { get; set; } = default!;
    public required string Name { get; set; }
    
    public int DifficultyClass { get; set; }  
    
    public bool isCraftingMaterial  { get; set; }
    public bool isEdible { get; set; }
    
}