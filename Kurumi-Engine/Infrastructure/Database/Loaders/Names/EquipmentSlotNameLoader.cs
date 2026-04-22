using Infrastructure.Database.Base;
using Infrastructure.Database.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Names;
using Infrastructure.Database.Repositories.Rows.Generic;

namespace Infrastructure.Database.Loaders.Names;

public sealed class EquipmentSlotNameLoader : IDataLoader<NamedData> 
{
    private readonly EquipmentSlotNameRepository _equipmentSlotNameRepository;
    private readonly NamedDataFactory _equipmentSlotNameFactory;

    public EquipmentSlotNameLoader(
        EquipmentSlotNameRepository equipmentSlotNameRepository, 
        NamedDataFactory equipmentSlotNameFactory) 
    {
        _equipmentSlotNameRepository = equipmentSlotNameRepository;
        _equipmentSlotNameFactory = equipmentSlotNameFactory;
    }

    /// <summary>
    /// Function that loads and returns the read only list of equipment slot name.
    /// </summary>
    /// <returns>The read only list of equipment slot name.</returns>
    public IReadOnlyList<NamedData> LoadAll() 
    {
        NameRow[] rows = _equipmentSlotNameRepository.LoadAll();
        var names = new NamedData[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            names[index] = _equipmentSlotNameFactory.Create(
                row.Id,
                row.Name
            );
        }
        return names;
    }
}