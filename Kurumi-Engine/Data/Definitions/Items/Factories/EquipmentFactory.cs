using Data.Definitions.Items.Core;
using Data.Definitions.Modifiers.Base;
using Data.Definitions.Modifiers.Core;

namespace Data.Definitions.Items.Factories;

public sealed class EquipmentFactory 
{
    public Equipment Create(
        int id, 
        int itemId, 
        int equipmentSlotId, 
        int equipmentTypeId, 
        int accuracy, 
        int evasion,
        string? turnEffectScript, 
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

        if (turnEffectScript != null) 
        {
            entityModifiers.Add(ModifierType.TurnEffect, new TurnEffect() {TurnScriptName = turnEffectScript});
        }

        return new Equipment()
        {
            Id = id, 
            ItemId = itemId, 
            EquipmentSlotId = 
            equipmentSlotId, 
            EquipmentTypeId = equipmentTypeId, 
            EntityModifiers = entityModifiers
        };
    }
}