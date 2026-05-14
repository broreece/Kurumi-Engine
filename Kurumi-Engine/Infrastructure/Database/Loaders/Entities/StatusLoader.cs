using Data.Definitions.Entities.Statuses.Core;
using Data.Definitions.Entities.Statuses.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Statuses;
using Infrastructure.Database.Repositories.Rows.Generic;
using Infrastructure.Database.Repositories.Rows.Statuses;

namespace Infrastructure.Database.Loaders.Entities;

public sealed class StatusLoader : IDataLoader<StatusDefinition> 
{
    private readonly StatusRepository _statusRepository;
    private readonly StatusAbilityRepository _statusAbilityRepository;
    private readonly StatusAbilitySetRepository _statusAbilitySetRepository;
    private readonly StatusElementRepository _statusElementRepository;
    private readonly StatusStatRepository _statusStatRepository;
    private readonly StatusDefinitionFactory _statusFactory;
    
    public StatusLoader(
        StatusRepository statusRepository, 
        StatusAbilityRepository statusAbilityRepository, 
        StatusAbilitySetRepository statusAbilitySetRepository, 
        StatusElementRepository statusElementRepository, 
        StatusStatRepository statusStatRepository, 
        StatusDefinitionFactory statusFactory) 
    {
        _statusRepository = statusRepository;
        _statusAbilityRepository = statusAbilityRepository;
        _statusAbilitySetRepository = statusAbilitySetRepository;
        _statusElementRepository = statusElementRepository;
        _statusStatRepository = statusStatRepository;
        _statusFactory = statusFactory;
    }

    public IReadOnlyList<StatusDefinition> LoadAll() 
    {
        StatusRow[] rows = _statusRepository.LoadAll();
        var statuses = new StatusDefinition[rows.Length];

        // Create lookup tables for the entity's additional data.
        ILookup<int, AbilitySealRow> statusAbilityLookup = _statusAbilityRepository.LoadAll().ToLookup(
            abilityRow => abilityRow.SourceId
        );
        ILookup<int, AbilitySealRow> statusAbilitySetLookup = _statusAbilitySetRepository.LoadAll().ToLookup(
            abilitySetRow => abilitySetRow.SourceId
        );
        ILookup<int, ObjectAttributeValueRow> statusElementsLookup = _statusElementRepository.LoadAll().ToLookup(
            elementRow => elementRow.ObjectId
        );
        ILookup<int, ObjectAttributeValueRow> statusStatsLookup = _statusStatRepository.LoadAll().ToLookup(
            statRow => statRow.ObjectId
        );

        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            var id = row.Id;
            // Ability sets.
            var sealedAbilitySets = new List<int>();
            foreach (var statusAbilitySet in statusAbilitySetLookup[id]) 
            {
                sealedAbilitySets.Add(statusAbilitySet.AbilityRefId);
            }
            // Abilities.
            var sealedAbilities = new List<int>();
            var addedAbilities = new List<int>();
            foreach (var statusAbility in statusAbilityLookup[id]) 
            {
                if (statusAbility.Sealed) 
                {
                    sealedAbilities.Add(statusAbility.AbilityRefId);
                }
                else 
                {
                    addedAbilities.Add(statusAbility.AbilityRefId);
                }
            }
            // Elements.
            var elements = new Dictionary<int, int>();
            foreach (var elementRow in statusElementsLookup[id]) 
            {
                elements[elementRow.AttributeId] = elementRow.Value;
            }
            // Stats.
            var stats = new Dictionary<int, int>();
            foreach (var statRow in statusStatsLookup[id]) 
            {
                stats[statRow.AttributeId] = statRow.Value;
            }
            statuses[index] = _statusFactory.Create(
                id,
                row.TurnLength,
                row.Priority,
                row.Accuracy,
                row.Evasion,
                row.SpriteName,
                row.TurnEffectScript,
                row.Name,
                row.Description,
                row.CanAct,
                row.CureAtBattleEnd,
                stats,
                elements,
                sealedAbilitySets,
                sealedAbilities,
                addedAbilities
            );
        }
        
        return statuses;
    }
}