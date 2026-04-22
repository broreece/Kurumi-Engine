using Infrastructure.Database.Base;
using Infrastructure.Database.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Names;
using Infrastructure.Database.Repositories.Rows.Generic;

namespace Infrastructure.Database.Loaders.Names;

public sealed class EquipmentTypeNameLoader : IDataLoader<NamedData> 
{
    public EquipmentTypeNameLoader(
        EquipmentTypeNameRepository equipmentTypeNameRepository, 
        NamedDataFactory equipmentTypeNameFactory) 
    {
        _equipmentTypeNameRepository = equipmentTypeNameRepository;
        _equipmentTypeNameFactory = equipmentTypeNameFactory;
    }

    public IReadOnlyList<NamedData> LoadAll() 
    {
        NameRow[] rows = _equipmentTypeNameRepository.LoadAll();
        var names = new NamedData[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            names[index] = _equipmentTypeNameFactory.Create(
                row.Id,
                row.Name
            );
        }
        return names;
    }

    private readonly EquipmentTypeNameRepository _equipmentTypeNameRepository;
    private readonly NamedDataFactory _equipmentTypeNameFactory;
}