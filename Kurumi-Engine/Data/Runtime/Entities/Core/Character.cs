using Data.Definitions.Entities.Core;
using Data.Runtime.Entities.Base;
using Data.Models.Characters;
using Engine.Systems.Statuses.Interfaces;

namespace Data.Runtime.Entities.Core;

public sealed class Character : IStats, IHasStatuses 
{
    private readonly CharacterDefinition _definition;
    private readonly CharacterModel _model;

    internal Character(CharacterDefinition definition, CharacterModel model) 
    {
        _definition = definition;
        _model = model;
    }

    public string Name => _definition.Name;

    public string BattleSpriteName => _definition.BattleSprite;

    public int MaxHP => _model.MaxHP;

    public int MaxMP => _model.MaxMP;

    public int CurrentHP 
    {
        get => _model.CurrentHP;
        set => _model.CurrentHP = value;
    }

    public int CurrentMP => _model.CurrentMP;

    public IReadOnlyList<int> GetStats() => _model.Stats;

    public List<int> GetAbilityIDs() => _model.Abilities;

    public Dictionary<int, List<int>> GetAbilitySetIDs() => _model.AbilitySets;

    public List<int> GetStatuses() => _model.Statuses;
}
