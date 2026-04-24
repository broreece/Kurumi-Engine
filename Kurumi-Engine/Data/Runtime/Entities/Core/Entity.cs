using Data.Definitions.Entities.Core;
using Data.Runtime.Entities.Base;
using Data.Models.Formations;
using Engine.Systems.Statuses.Interfaces;

namespace Data.Runtime.Entities.Core;

public sealed class Entity : IStats, IHasStatuses 
{
    private readonly EntityDefinition _definition;
    private readonly EnemyModel _model;

    public required int CurrentHP { get; set; }

    public string SpriteName => _definition.SpriteName;

    internal Entity(EntityDefinition definition, EnemyModel model) 
    {
        _definition = definition;
        _model = model;
    }

    public IReadOnlyList<int> GetStats() => _definition.Stats;

    public List<int> GetStatuses() => _model.Statuses;
}
