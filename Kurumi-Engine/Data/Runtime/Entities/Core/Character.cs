using Data.Definitions.Entities.Core;
using Data.Models.Characters;
using Data.Runtime.Entities.Base;
using Data.Runtime.Entities.Statuses.Core;

using Engine.Systems.Statuses.Interfaces;

namespace Data.Runtime.Entities.Core;

public sealed class Character : IStats, IHasStatuses 
{
    private readonly CharacterDefinition _definition;
    private readonly CharacterModel _model;

    private readonly List<Status> _statuses;

    internal Character(CharacterDefinition definition, CharacterModel model) 
    {
        _definition = definition;
        _model = model;
        
        _statuses = [];
    }

    public string Name => _definition.Name;

    public string BattleSpriteName => _definition.BattleSprite;

    public int Id => _definition.Id;

    public int MaxMP => _model.MaxMP;

    // TODO: Hard coded value here to be replaced with configurable setting.
    public int BattleSpeed => _model.Stats[2];

    public int CurrentHP 
    {
        get => _model.CurrentHP;
        set => _model.CurrentHP = value;
    }

    public int CurrentMP => _model.CurrentMP;

    public int GetMaxHp() => _model.MaxHP;

    public IReadOnlyList<int> GetStats() => _model.Stats;

    public IList<int> GetAbilityIDs() => _model.Abilities;

    public Dictionary<int, List<int>> GetAbilitySetIDs() => _model.AbilitySets;

    public void ClearStatuses() => _statuses.Clear();

    public void AddStatus(Status newStatus) => _statuses.Add(newStatus);

    public IList<Status> GetStatuses() => _statuses;
}
