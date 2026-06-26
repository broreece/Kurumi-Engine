// Data.
using Data.Models.Characters.Core;
using Data.Models.Characters.Exceptions;

namespace Data.Models.Characters.Collections;

public sealed class CharacterModelCollection
{
    public required Dictionary<int, CharacterModel> Characters { get; set; }

    public CharacterModel Get(int characterId)
    {
        if (Characters.TryGetValue(characterId, out var characterModel))
        {
            return characterModel;
        }
        throw new CharacterNotFoundException($"No Formation found with the key: {characterId}");
    }
}