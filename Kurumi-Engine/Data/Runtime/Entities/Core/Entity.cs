using Data.Definitions.Entities.Core;
using Data.Models.Formations;
using Data.Runtime.Entities.Base;
using Data.Runtime.Entities.Statuses.Core;

using Engine.Systems.Statuses.Interfaces;

namespace Data.Runtime.Entities.Core;

public sealed class Entity : IStats, IHasStatuses 
{
    private readonly EntityDefinition _definition;
    private readonly EnemyModel _model;

    private readonly List<Status> _statuses;

    public required int CurrentHP { get; set; }

    public int Id => _definition.Id;

    // TODO: Hard coded value here to be replaced with configurable setting.
    public int BattleSpeed => _definition.Stats[2];

    public string SpriteName => _definition.SpriteName;

    internal Entity(EntityDefinition definition, EnemyModel model) 
    {
        _definition = definition;
        _model = model;

        _statuses = [];
    }

    public int GetMaxHp() => _definition.MaxHp;

    public IReadOnlyList<int> GetStats() => _definition.Stats;

    public void ClearStatuses() => _statuses.Clear();

    public void AddStatus(Status newStatus) => _statuses.Add(newStatus);

    public IList<Status> GetStatuses() => _statuses;
}
