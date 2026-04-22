using Data.Definitions.Entities.Core;

namespace Data.Definitions.Entities.Factories;

public sealed class CharacterDefinitionFactory 
{
    public CharacterDefinition Create(
        int id, 
        string battleSprite, 
        string fieldSprite, 
        string menuSprite, 
        string name, 
        string description) 
    {
        return new CharacterDefinition()
        {
            Id = id, 
            BattleSprite = battleSprite, 
            FieldSprite = fieldSprite, 
            MenuSprite = menuSprite, 
            Name = name, 
            Description = description
        };
    }
}