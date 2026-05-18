using Data.Definitions.Entities.Core;
using Data.Models.Formations;
using Data.Runtime.Entities.Core;

namespace Data.Runtime.Entities.Factories;

public sealed class EntityFactory 
{
    private readonly int _agilityIndex;

    public EntityFactory(int agilityIndex)
    {
        _agilityIndex = agilityIndex;
    }

    public Entity Create(EntityDefinition definition, EnemyModel model, int currentHp) 
    {
        return new Entity(definition, model, _agilityIndex) { CurrentHP = currentHp };
    }
}