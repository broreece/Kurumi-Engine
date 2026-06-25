// Data.
using Data.Definitions.Entities.Core;

using Data.Models.Characters.Core;

using Data.Runtime.Entities.Base;
using Data.Runtime.Entities.Statuses.Core;

// Engine.
using Engine.Systems.Statuses.Interfaces;

namespace Data.Runtime.Entities.Core;

public sealed class Character : IStats, IHasStatuses 
{
    private readonly CharacterDefinition _definition;
    private readonly CharacterModel _model;

    private readonly int _agilityIndex;

    public string Name => _definition.Name;

    public string BattleSpriteName => _definition.BattleSprite;

    public int Id => _definition.Id;

    public int MaxMP => _model.MaxMP;

    public int CurrentMP => _model.CurrentMP;

    public int BattleSpeed => _model.Stats[_agilityIndex];

    public int MaxHp => _model.MaxHP;

    public int CurrentHP 
    {
        get => _model.CurrentHP;
        set => _model.CurrentHP = value;
    }

    public IList<Status> Statuses { get; } = [];

    public IReadOnlyList<int> Stats => _model.Stats;

    public IList<int> AbilityIDs => _model.Abilities;

    public IDictionary<int, List<int>> AbilitySetIDs => _model.AbilitySets;

    internal Character(CharacterDefinition definition, CharacterModel model, int agilityIndex) 
    {
        _definition = definition;
        _model = model;

        _agilityIndex = agilityIndex;
    }

    public void ClearStatuses() => Statuses.Clear();

    public void AddStatus(Status newStatus) => Statuses.Add(newStatus);
}
