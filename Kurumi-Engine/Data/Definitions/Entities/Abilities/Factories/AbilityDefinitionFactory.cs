using Data.Definitions.Entities.Abilities.Core;

namespace Data.Definitions.Entities.Abilities.Factories;

public sealed class AbilityDefinitionFactory 
{
    public AbilityDefinition Create(
        int id, 
        string name, 
        string description, 
        string? scriptName, 
        int elementId, 
        int cost,
        bool usesMp, 
        string spriteName) 
    {
        return new AbilityDefinition()
        {
            Id = id, 
            Name = name, 
            Description = description, 
            ScriptName = scriptName, 
            ElementId = elementId, 
            Cost = cost, 
            UsesMp = usesMp, 
            SpriteName = spriteName
        };
    }
}