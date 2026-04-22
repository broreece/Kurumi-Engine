using Infrastructure.Database.Base;
using Infrastructure.Database.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Names;
using Infrastructure.Database.Repositories.Rows.Generic;

namespace Infrastructure.Database.Loaders.Names;

public sealed class ElementNameLoader : IDataLoader<NamedData> 
{
    private readonly ElementNameRepository _elementNameRepository;
    private readonly NamedDataFactory _elementNameFactory;

    public ElementNameLoader(ElementNameRepository elementNameRepository, NamedDataFactory elementNameFactory) 
    {
        _elementNameRepository = elementNameRepository;
        _elementNameFactory = elementNameFactory;
    }

    public IReadOnlyList<NamedData> LoadAll() 
    {
        NameRow[] rows = _elementNameRepository.LoadAll();
        var names = new NamedData[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            names[index] = _elementNameFactory.Create(
                row.Id,
                row.Name
            );
        }
        return names;
    }
}