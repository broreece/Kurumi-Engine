// Data.
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
        bool useableInMenu, 
        bool defaultTargetParty, 
        bool randomTarget, 
        bool targetsAll, 
        string spriteName
    ) 
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
            UseableInMenu = useableInMenu, 
            DefaultTargetParty = defaultTargetParty, 
            RandomTarget = randomTarget, 
            TargetsAll = targetsAll, 
            SpriteName = spriteName
        };
    }
}