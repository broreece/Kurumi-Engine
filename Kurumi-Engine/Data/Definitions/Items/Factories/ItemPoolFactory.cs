// Data.
using Data.Definitions.Items.Core;

namespace Data.Definitions.Items.Factories;

public sealed class ItemPoolFactory 
{
    public ItemPool Create(int id, IReadOnlyList<int> itemIds) 
    {
        return new ItemPool() { Id = id, ItemIds = itemIds };
    }
}