using Data.Definitions.Formations.Core;
using Data.Models.Formations;
using Data.Runtime.Formations.Base;

namespace Data.Runtime.Formations.Core;

public sealed class Formation 
{
    private readonly FormationDefinition _definition;
    private readonly FormationModel _model;

    public required IReadOnlyList<StoredEntityData> StoredEntityData { get; init; }

    internal Formation(
        FormationDefinition definition, 
        FormationModel model
    ) 
    {
        _definition = definition;
        _model = model;
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
}
