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
        //Stops process if any data exists in components table
        if (! await db.Components.AnyAsync())
        {
            List<Component>? components = LoadJsonComponents();
            if (components != null)
            {
                foreach (var component in components)
                {
                    component.Id = Guid.NewGuid();
                }
                db.AddRange(components);
            }
        } 
        
        //CCLinks
        if (! await db.CCLinks.AnyAsync())
        {
            List<CCLink>? ccLinks = await LoadJsonCCLinks(db);
            if (ccLinks != null)
            {
                db.AddRange(ccLinks);
            }
        } 
        
        
        
        await db.SaveChangesAsync();
    }

    private static List<Component>? LoadJsonComponents()
    {
        string json = File.ReadAllText(@"Services\Database\Components.json");
        List<Component>? components = JsonSerializer.Deserialize<List<Component>>(json);
        return components;
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

    private static async Task<List<CCLink>?> LoadJsonCCLinks(AppDb db)
    {
        List<CCLink>? ccLinks = new List<CCLink>();
        string json = File.ReadAllText(@"Services\Database\CCLinks.json");
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

                string componentName = compNameEl.GetString() ?? "";
                if (string.IsNullOrWhiteSpace(componentName)) continue;

                int dc = dcEl.GetInt32();

                Guid componentGuid = await GetComponentIdFromDb(db, componentName);

                CCLink link = new CCLink() { CreatureTypeId = creatureTypeGuid, ComponentId = componentGuid, DifficultyClass = dc};
                ccLinks.Add(link);
            }
        }

        if (ccLinks.Count == 0) { ccLinks = null;}
        return ccLinks;
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