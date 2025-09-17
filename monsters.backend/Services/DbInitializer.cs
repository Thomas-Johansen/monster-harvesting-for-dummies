using Microsoft.EntityFrameworkCore;
using monsters.backend.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace monsters.backend.Services;


//Testing stuffs
public class DbInitializer
{
    public static async Task SeedAsync(AppDb db)
    {
        await db.Database.MigrateAsync();
        

        //Creature Type
        if (! await db.CreatureTypes.AnyAsync())
        {
            List<CreatureType>? creatureTypes = LoadJsonCreatureTypes();
            if (creatureTypes != null)
            {
                foreach (var component in creatureTypes)
                {
                    component.Id = Guid.NewGuid();
                }
                db.AddRange(creatureTypes);
            }
        } 
        
        
        //Components
        if (! await db.Components.AnyAsync())
        {
            List<Component>? components = await LoadJsonComponents(db);
            if (components != null)
            {
                db.AddRange(components);
            }
        } 
        
        
        
        await db.SaveChangesAsync();
    }
    
    private static List<CreatureType>? LoadJsonCreatureTypes()
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };
        string json = File.ReadAllText(@"Services\Database\CreatureTypes.json");
        List<CreatureType>? creatureTypes = JsonSerializer.Deserialize<List<CreatureType>>(json, options);
        return creatureTypes;
    }

    private static async Task<List<Component>?> LoadJsonComponents(AppDb db)
    {
        List<Component>? components = new List<Component>();
        string json = File.ReadAllText(@"Services\Database\Components.json");
        using var doc = JsonDocument.Parse(json);
        JsonElement root = doc.RootElement;

        foreach (var creatureType in root.EnumerateObject())
        {
            string creatureTypeName = creatureType.Name;
            JsonElement creatureTypeElement = creatureType.Value;
            //Console.WriteLine (creatureTypeName);
            Guid creatureTypeGuid = await GetTypeIdFromDb(db, creatureTypeName);

            foreach (var component in creatureTypeElement.EnumerateArray())
            {
                if (!component.TryGetProperty("Name", out var compNameEl)) continue;
                if (!component.TryGetProperty("DifficultyClass", out var dcEl)) continue;
                if (!component.TryGetProperty("isCraftingMaterial", out var compIsCraftingMaterialEl)) continue;
                if (!component.TryGetProperty("isEdible", out var compIsEdibleEl)) continue;

                string componentName = compNameEl.GetString() ?? "";
                if (string.IsNullOrWhiteSpace(componentName)) continue;

                int dc = dcEl.GetInt32();
                
                bool isCraftingMaterial = compIsCraftingMaterialEl.GetBoolean();
                bool isEdible = compIsEdibleEl.GetBoolean();


                Component link = new Component()
                {
                    Id = new Guid(),
                    CreatureTypeId = creatureTypeGuid,
                    Name = componentName,
                    DifficultyClass = dc,
                    isCraftingMaterial = isCraftingMaterial,
                    isEdible = isEdible,
                };
                components.Add(link);
            }
        }

        if (components.Count == 0) { components = null;}
        return components;
    }

    private static async Task<Guid> GetTypeIdFromDb(AppDb db, string name, CancellationToken ct = default)
    {
        return await db.CreatureTypes
            .Where(t => t.Name == name)
            .Select(t => t.Id)
            .SingleOrDefaultAsync(ct);
    }
    private static async Task<Guid> GetComponentIdFromDb(AppDb db, string name, CancellationToken ct = default)
    {
        return await db.Components
            .Where(t => t.Name == name)
            .Select(t => t.Id)
            .SingleOrDefaultAsync(ct);
    }
}