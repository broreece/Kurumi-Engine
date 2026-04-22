using Data.Definitions.Entities.Core;
using Data.Definitions.Entities.Factories;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Repositories.Core.Characters;
using Infrastructure.Database.Repositories.Rows.Characters;

namespace Infrastructure.Database.Loaders.Entities;

public sealed class CharacterLoader : IDataLoader<CharacterDefinition> 
{
    public CharacterLoader(CharacterRepository characterRepository, 
        CharacterDefinitionFactory characterDefinitionFactory) 
    {
        _characterRepository = characterRepository;
        _characterDefinitionFactory = characterDefinitionFactory;
    }

    public IReadOnlyList<CharacterDefinition> LoadAll() 
    {
        CharacterRow[] rows = _characterRepository.LoadAll();
        var characters = new CharacterDefinition[rows.Length];
        for (var index = 0; index < rows.Length; index ++) 
        {
            var row = rows[index];
            characters[index] = _characterDefinitionFactory.Create(
                row.Id,
                row.BattleSprite,
                row.FieldSprite,
                row.MenuSprite,
                row.Name,
                row.Description
            );
        }
        return characters;
    }

    private readonly CharacterRepository _characterRepository;
    private readonly CharacterDefinitionFactory _characterDefinitionFactory;
}