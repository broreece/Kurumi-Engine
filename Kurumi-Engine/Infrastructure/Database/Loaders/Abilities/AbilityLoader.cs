using Data.Definitions.Entities.Abilities.Core;
using Data.Definitions.Entities.Abilities.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Abilities;
using Infrastructure.Database.Repositories.Rows.Abilities;

namespace Infrastructure.Database.Loaders.Abilities;

public sealed class AbilityLoader : IDataLoader<AbilityDefinition> 
{
    private readonly AbilityRepository _abilityRepository;
    private readonly AbilityDefinitionFactory _abilityFactory;

    public AbilityLoader(AbilityRepository abilityRepository, AbilityDefinitionFactory abilityFactory) 
    {
        _abilityRepository = abilityRepository;
        _abilityFactory = abilityFactory;
    }

    public IReadOnlyList<AbilityDefinition> LoadAll() 
    {
        AbilityRow[] rows = _abilityRepository.LoadAll();
        var abilities = new AbilityDefinition[rows.Length];
        for (int index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            abilities[index] = _abilityFactory.Create(
                row.Id,
                row.Name,
                row.Description,
                row.ScriptName,
                row.ElementId,
                row.Cost,
                row.UsesMp,
                row.BattleSpriteAnimationName
            );
        }
        return abilities;
    }
}