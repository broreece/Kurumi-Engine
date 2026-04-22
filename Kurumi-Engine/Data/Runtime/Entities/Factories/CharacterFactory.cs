using Data.Definitions.Entities.Core;
using Data.Models.Characters;
using Data.Runtime.Entities.Core;

namespace Data.Runtime.Entities.Factories;

public sealed class CharacterFactory 
{
    public Character Create(CharacterDefinition definition, CharacterModel model) 
    {
        return new Character(definition, model);
    }
}