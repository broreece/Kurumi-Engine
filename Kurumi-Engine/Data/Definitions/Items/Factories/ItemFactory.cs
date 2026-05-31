// Data.
using Data.Definitions.Items.Core;

namespace Data.Definitions.Items.Factories;

public sealed class ItemFactory 
{
    public Item Create(
        int id, 
        int price, 
        int weight, 
        string spriteName, 
        string? scriptName, 
        string name, 
        string description, 
        bool useableInBattle, 
        bool useableInMenu, 
        bool defaultTargetParty, 
        bool randomTarget, 
        bool targetsAll, 
        bool consumeOnUse
    ) 
    {
        return new Item() 
        {
            Id = id, 
            Price = price, 
            Weight = weight, 
            SpriteName = spriteName, 
            ScriptName = scriptName, 
            Name = name, 
            Description = description, 
            UseableInBattle = useableInBattle, 
            UseableInMenu = useableInMenu, 
            DefaultTargetParty = defaultTargetParty, 
            RandomTarget = randomTarget, 
            TargetsAll = targetsAll, 
            ConsumeOnUse = consumeOnUse
        };
    }
}