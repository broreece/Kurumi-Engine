using Data.Definitions.Entities.Core;
using Data.Definitions.Entities.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Entities;
using Infrastructure.Database.Repositories.Rows.Entities;
using Infrastructure.Database.Repositories.Rows.Generic;

namespace Infrastructure.Database.Loaders.Entities;

public sealed class EntityDefinitionLoader : IDataLoader<EntityDefinition> 
{
    private readonly EntityRepository _entityRepository;
    private readonly EntityAbilityRepository _entityAbilityRepository;
    private readonly EntityElementRepository _entityElementRepository;
    private readonly EntityStatRepository _entityStatRepository;
    private readonly EntityStatusRepository _entityStatusRepository;
    private readonly EntityDefinitionFactory _entityDefinitionFactory;

    public EntityDefinitionLoader(
        EntityRepository entityRepository, 
        EntityAbilityRepository entityAbilityRepository,
        EntityElementRepository entityElementRepository, 
        EntityStatRepository entityStatRepository,
        EntityStatusRepository entityStatusRepository,
        EntityDefinitionFactory entityDefinitionFactory) 
    {
        _entityRepository = entityRepository;
        _entityAbilityRepository = entityAbilityRepository;
        _entityElementRepository = entityElementRepository;
        _entityStatRepository = entityStatRepository;
        _entityStatusRepository = entityStatusRepository;
        _entityDefinitionFactory = entityDefinitionFactory;
    }

    public IReadOnlyList<EntityDefinition> LoadAll() 
    {
        EntityRow[] rows = _entityRepository.LoadAll();
        var entities = new EntityDefinition[rows.Length];

        // Create lookup tables for the entity's additional data.
        ILookup<int, EntityAbilityRow> entityAbilityLookup = _entityAbilityRepository.LoadAll().ToLookup(
            abilityRow => abilityRow.EntityIndex
        );
        ILookup<int, ObjectAttributeValueRow> entityElementsLookup = _entityElementRepository.LoadAll().ToLookup(
            elementRow => elementRow.ObjectId
        );
        ILookup<int, ObjectAttributeValueRow> entityStatsLookup = _entityStatRepository.LoadAll().ToLookup(
            statRow => statRow.ObjectId
        );
        ILookup<int, ObjectAttributeValueRow> entityStatusLookup = _entityStatusRepository.LoadAll().ToLookup(
            statusRow => statusRow.ObjectId
        );

        for (int index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            var id = row.Id;
            // Abilities.
            var entityAbilities = new List<int>();
            foreach (var entityAbility in entityAbilityLookup[id]) 
            {
                entityAbilities.Add(entityAbility.AbilityId);
            }
            // For array values subtract attribute ID by 1 as database IDs start at 1.
            // Elements.
            var elements = new int[entityElementsLookup[id].Count()];
            foreach (var elementRow in entityElementsLookup[id]) 
            {
                elements[elementRow.AttributeId - 1] = elementRow.Value;
            }
            // Stats.
            var stats = new int[entityStatsLookup[id].Count()];
            foreach (var statRow in entityStatsLookup[id]) 
            {
                stats[statRow.AttributeId - 1] = statRow.Value;
            }
            // Statuses.
            var statuses = new int[entityStatusLookup[id].Count()];
            foreach (var statusRow in entityStatusLookup[id]) 
            {
                statuses[statusRow.AttributeId - 1] = statusRow.Value;
            }
            entities[index] = _entityDefinitionFactory.Create(
                id,
                row.MaxHP,
                row.BattleSpriteName,
                row.Name,
                row.Description,
                stats,
                elements,
                statuses,
                entityAbilities
            );
        }
        return entities;
    }
}