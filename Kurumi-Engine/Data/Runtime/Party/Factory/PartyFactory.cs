namespace Data.Runtime.Party.Factory;

using Data.Definitions.Entities.Core;
using Data.Models.Characters;
using Data.Models.Party;
using Data.Runtime.Entities.Core;
using Data.Runtime.Party.Core;
using Infrastructure.Database.Base;

public sealed class PartyFactory 
{
    private readonly Dictionary<int, CharacterModel> _characterModels;
    private readonly Registry<CharacterDefinition> _characterRegistry;

    private readonly int _maxPartySize;

    public PartyFactory(
        Dictionary<int, CharacterModel> characterModels, 
        Registry<CharacterDefinition> characterRegistry, 
        int maxPartySize)
    {
        _characterModels = characterModels;
        _characterRegistry = characterRegistry;
        _maxPartySize = maxPartySize;
    }

    public Party Create(PartyModel partyModel, Dictionary<int, int> inventory) 
    {
        // Load the character models from the save data's character dictionary.
        var characters = new Character[_maxPartySize];
        var characterIds = partyModel.PartyMembers;
        for (var index = 0; index < characterIds.Count; index ++) 
        {
            var characterId = characterIds[index];
            characters[index] = new Character(_characterRegistry.Get(characterId), _characterModels[characterId]);
        }
        return new Party() {Characters = characters, PartyModel = partyModel, Inventory = inventory};
    }
}