using Infrastructure.Database.Base;
using Infrastructure.Database.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Abilities;
using Infrastructure.Database.Repositories.Rows.Generic;

namespace Infrastructure.Database.Loaders.Abilities;

public sealed class AbilitySetLoader : IDataLoader<NamedData> 
{
    private readonly AbilitySetRepository _abilitySetRepository;
    private readonly NamedDataFactory _abilitySetFactory;

    public AbilitySetLoader(AbilitySetRepository abilitySetRepository, NamedDataFactory abilitySetFactory) 
    {
        _abilitySetRepository = abilitySetRepository;
        _abilitySetFactory = abilitySetFactory;
    }

    public IReadOnlyList<NamedData> LoadAll() 
    {
        NameRow[] rows = _abilitySetRepository.LoadAll();
        var abilitySets = new NamedData[rows.Length];
        for (int index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            abilitySets[index] = _abilitySetFactory.Create(
                row.Id,
                row.Name
            );
        }
        return abilitySets;
    }
}