using Data.Definitions.Formations.Core;
using Data.Models.Formations;
using Data.Runtime.Entities.Core;

namespace Data.Runtime.Formations.Core;

public sealed class Formation 
{
    private readonly FormationDefinition _definition;
    private readonly FormationModel _model;

    private readonly IReadOnlyList<Entity> _entities;

    internal Formation(FormationDefinition definition, FormationModel model, IReadOnlyList<Entity> entities) 
    {
        _definition = definition;
        _model = model;
        _entities = entities;
    }

    public bool IsDefeated()
    {
        foreach (var enemy in _entities)
        {
            if (enemy.CurrentHP > 0)
            {
                return false;
            }
        }
        return true;
    }
}
