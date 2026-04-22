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
        bool usableInBattle, 
        bool usableInMenu, 
        bool targetsParty, 
        bool targetsEnemies, 
        bool targetsAll, 
        bool consumeOnUse) 
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
            UsableInBattle = usableInBattle, 
            UsableInMenu = usableInMenu, 
            TargetsParty = targetsParty, 
            TargetsEnemies = targetsEnemies, 
            TargetsAll = targetsAll, 
            ConsumeOnUse = consumeOnUse
        };
    }
}