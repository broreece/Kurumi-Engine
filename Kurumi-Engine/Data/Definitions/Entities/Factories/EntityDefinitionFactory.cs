using Data.Definitions.Entities.Core;

namespace Data.Definitions.Entities.Factories;

public sealed class EntityDefinitionFactory 
{
    public EntityDefinition Create(
        int id, 
        int maxHp, 
        string spriteName, 
        string name, 
        string description,
        int[] stats, 
        int[] elementalResistances, 
        int[] statusResistances, 
        List<int> baseAbilities) 
    {
        return new EntityDefinition()
        {
            Id = id, 
            MaxHp = maxHp, 
            SpriteName = spriteName, 
            Name = name, 
            Description = description, 
            Stats = stats, 
            ElementalResistances = elementalResistances, 
            StatusResistances = statusResistances, 
            BaseAbilities = baseAbilities
        };
    }
}