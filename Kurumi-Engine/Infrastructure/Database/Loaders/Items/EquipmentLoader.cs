using Data.Definitions.Items.Core;
using Data.Definitions.Items.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Equipment;
using Infrastructure.Database.Repositories.Rows.Equipment;
using Infrastructure.Database.Repositories.Rows.Generic;

namespace Infrastructure.Database.Loaders.Items;

public sealed class EquipmentLoader : IDataLoader<Equipment> 
{
    private readonly EquipmentRepository _equipmentRepository;
    private readonly EquipmentAbilityRepository _equipmentAbilityRepository;
    private readonly EquipmentAbilitySetRepository _equipmentAbilitySetRepository;
    private readonly EquipmentElementRepository _equipmentElementRepository;
    private readonly EquipmentStatRepository _equipmentStatRepository;
    private readonly EquipmentStatusRepository _equipmentStatusRepository;
    private readonly EquipmentFactory _equipmentFactory;

    public EquipmentLoader(
        EquipmentRepository equipmentRepository, 
        EquipmentAbilityRepository equipmentAbilityRepository, 
        EquipmentAbilitySetRepository equipmentAbilitySetRepository, 
        EquipmentElementRepository equipmentElementRepository, 
        EquipmentStatRepository equipmentStatRepository,
        EquipmentStatusRepository equipmentStatusRepository, 
        EquipmentFactory equipmentFactory) 
    {
        _equipmentRepository = equipmentRepository;
        _equipmentAbilityRepository = equipmentAbilityRepository;
        _equipmentAbilitySetRepository = equipmentAbilitySetRepository;
        _equipmentElementRepository = equipmentElementRepository;
        _equipmentStatRepository = equipmentStatRepository;
        _equipmentStatusRepository = equipmentStatusRepository;
        _equipmentFactory = equipmentFactory;
    }

    public IReadOnlyList<Equipment> LoadAll() 
    {
        EquipmentRow[] rows = _equipmentRepository.LoadAll();
        var equipment = new Equipment[rows.Length];

        // Create lookup tables for the equipment's additional data.
        ILookup<int, AbilitySealRow> equipmentAbilityLookup = _equipmentAbilityRepository.LoadAll().ToLookup(
            abilityRow => abilityRow.SourceId
        );
        ILookup<int, AbilitySealRow> equipmentAbilitySetLookup = _equipmentAbilitySetRepository.LoadAll().ToLookup(
            abilitySetRow => abilitySetRow.SourceId
        );
        ILookup<int, ObjectAttributeValueRow> equipmentElementLookup = _equipmentElementRepository.LoadAll().ToLookup(
            elementRow => elementRow.ObjectId
        );
        ILookup<int, ObjectAttributeValueRow> equipmentStatLookup = _equipmentStatRepository.LoadAll().ToLookup(
            statRow => statRow.ObjectId
        );
        ILookup<int, ObjectAttributeValueRow> equipmentStatusLookup = _equipmentStatusRepository.LoadAll().ToLookup(
            statusRow => statusRow.ObjectId
        );

        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            var id = row.Id;
            // Abilities.
            var equipmentAddedAbilities = new List<int>();
            var equipmentSealedAbilities = new List<int>();
            foreach (var equipmentAbility in equipmentAbilityLookup[id]) 
            {
                if (equipmentAbility.Sealed) 
                {
                    equipmentSealedAbilities.Add(equipmentAbility.AbilityRefId);
                }
                else 
                {
                    equipmentAddedAbilities.Add(equipmentAbility.AbilityRefId);
                }
            }
            // Ability sets.
            var equipmentAbilitySets = new List<int>();
            foreach (var equipmentAbilitySet in equipmentAbilitySetLookup[id]) 
            {
                equipmentAbilitySets.Add(equipmentAbilitySet.AbilityRefId);
            }
            // Elements.
            var elements = new Dictionary<int, int>();
            foreach (var elementRow in equipmentElementLookup[id]) 
            {
                elements[elementRow.AttributeId] = elementRow.Value;
            }
            // Stats.
            var stats = new Dictionary<int, int>();
            foreach (var statRow in equipmentStatLookup[id]) 
            {
                stats[statRow.AttributeId] = statRow.Value;
            }
            // Statuses.
            var statuses = new Dictionary<int, int>();
            foreach (var statusRow in equipmentStatusLookup[id]) 
            {
                statuses[statusRow.AttributeId] = statusRow.Value;
            }
            equipment[index] = _equipmentFactory.Create(
                id,
                row.ItemId,
                row.EquipmentSlot,
                row.EquipmentType,
                row.Accuracy,
                row.Evasion,
                row.TurnEffectScript,
                stats,
                elements,
                equipmentAbilitySets,
                equipmentSealedAbilities,
                equipmentAddedAbilities
            );
        }
        
        return equipment;
    }
}