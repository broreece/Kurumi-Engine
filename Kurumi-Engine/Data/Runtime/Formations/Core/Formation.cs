using Data.Definitions.Formations.Core;

using Data.Models.Formations;

using Data.Runtime.Entities.Core;
using Data.Runtime.Formations.Base;

namespace Data.Runtime.Formations.Core;

public sealed class Formation 
{
    private readonly FormationDefinition _definition;
    private readonly FormationModel _model;

    // List of entities which contains the stats/statuses of the formation.
    private readonly IReadOnlyList<Entity> _entities;

    // List of enemies which contains the drawable location and battle scripts of the formation.
    public IReadOnlyList<Enemy> Enemies { get; }

    public required IReadOnlyList<StoredEntityData> StoredEntityData { get; init; }

    internal Formation(
        FormationDefinition definition, 
        FormationModel model,
        IReadOnlyList<Entity> entities,
        IReadOnlyList<Enemy> enemies
    ) 
    {
        _definition = definition;
        _model = model;
        _entities = entities;
        
        Enemies = enemies;
    }

    public bool IsDefeated()
    {
        foreach (var entityData in StoredEntityData)
        {
            if (entityData.Entity.CurrentHP > 0)
            {
                return false;
            }
        }
        return true;
    }

    public int GetAmountOfLivingEnemies()
    {
        var size = 0;
        foreach (var enemy in _entities)
        {
            if (enemy.CurrentHP > 0)
            {
                size ++;
            }
        }
        return size;
    }

    public Entity GetEntityAt(int entityIndex) => _entities[entityIndex];
}
