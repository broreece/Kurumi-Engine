namespace Data.Definitions.Entities.Status.Factories;

using Data.Definitions.Entities.Status.Core;
using Data.Definitions.Modifiers.Base;
using Data.Definitions.Modifiers.Core;

public sealed class StatusFactory 
{
    public Status Create(
        int id, 
        int maxTurns, 
        int priority, 
        int accuracy, 
        int evasion, 
        string spriteName, 
        string? turnEffectScriptName, 
        string name, 
        string description, 
        bool canAct, 
        bool cureAtEnd,
        IReadOnlyDictionary<int, int> stats, 
        IReadOnlyDictionary<int, int> elements, 
        IReadOnlyList<int> sealedAbilitySets, 
        IReadOnlyList<int> sealedAbilities, 
        IReadOnlyList<int> addedAbilities) 
    {

        // Create a dictionary for the modifications so it's faster to access then looping and checking types.
        Dictionary<ModifierType, IEntityModifier> entityModifiers = [];
        entityModifiers.Add(ModifierType.Ability, new AbilityModifier() 
        {
            SealedAbilitySets = sealedAbilitySets, 
            SealedAbilities = sealedAbilities, 
            AddedAbilities = addedAbilities
        });
        entityModifiers.Add(ModifierType.Element, new ElementModifier() {Elements = elements});
        entityModifiers.Add(ModifierType.HitChance, new HitChanceModifier() 
        {
            AccuracyModifier = accuracy, 
            EvasionModifier = evasion
        });
        entityModifiers.Add(ModifierType.Stat, new StatModifier() {Stats = stats});

        if (turnEffectScriptName != null) 
        {
            entityModifiers.Add(ModifierType.TurnEffect, new TurnEffect() {TurnScriptName = turnEffectScriptName});
        }
            
        return new Status()
        {
            Id = id, 
            MaxTurns = maxTurns, 
            Priority = priority, 
            SpriteName = spriteName, 
            Name = name, 
            Description = description, 
            CanAct = canAct, 
            CureAtEnd = cureAtEnd, 
            EntityModifiers = entityModifiers
        };
    }
}