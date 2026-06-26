// Data.
using Data.Definitions.Entities.Core;

using Data.Models.Formations.Core;

using Data.Runtime.Entities.Base;
using Data.Runtime.Entities.Statuses.Core;

// Engine.
using Engine.Systems.Statuses.Interfaces;

namespace Data.Runtime.Entities.Core;

public sealed class Entity : IStats, IHasStatuses 
{
    private readonly EntityDefinition _definition;
    private readonly EnemyModel _model;

    private readonly int _agilityIndex;

    public required int CurrentHP { get; set; }

    public int MaxHp => _definition.MaxHp;

    public int Id => _definition.Id;

    public int BattleSpeed => _definition.Stats[_agilityIndex];

    public string SpriteName => _definition.SpriteName;

    public IList<Status> Statuses { get; } = [];

    public IReadOnlyList<int> Stats => _definition.Stats;

    internal Entity(EntityDefinition definition, EnemyModel model, int agilityIndex) 
    {
        _definition = definition;
        _model = model;

        _agilityIndex = agilityIndex;
    }

    public void ClearStatuses() => Statuses.Clear();

    public void AddStatus(Status newStatus) => Statuses.Add(newStatus);
}
