// Data.
using Data.Definitions.Entities.Core;

using Data.Models.Characters.Collections;
using Data.Models.Inventory;
using Data.Models.Party;

using Data.Runtime.Entities.Core;
using Data.Runtime.Entities.Factories;
using Data.Runtime.Parties.Core;

// Infrastructure.
using Infrastructure.Database.Base;

namespace Data.Runtime.Parties.Factory;

public sealed class PartyFactory 
{
    private readonly CharacterModelCollection _characterModelCollection;
    private readonly Registry<CharacterDefinition> _characterRegistry;

    private readonly CharacterFactory _characterFactory;

    private readonly int _maxPartySize;

    public PartyFactory(
        CharacterModelCollection characterModelCollection, 
        Registry<CharacterDefinition> characterRegistry, 
        int maxPartySize,
        int agilityIndex
    )
    {
        _characterModelCollection = characterModelCollection;
        _characterRegistry = characterRegistry;
        _maxPartySize = maxPartySize;

        _characterFactory = new CharacterFactory(agilityIndex);
    }

    public Party Create(PartyModel partyModel, Inventory inventory, float movementSpeed) 
    {
        // Load the character models from the save data's character dictionary.
        var characters = new Character[_maxPartySize];
        var characterIds = partyModel.PartyMembers;
        for (var index = 0; index < characterIds.Count; index ++) 
        {
            var characterId = characterIds[index];
            characters[index] = _characterFactory.Create(
                _characterRegistry.Get(characterId), 
                _characterModelCollection.Get(characterId)
            );
        }
        return new Party() { 
            Characters = characters, 
            PartyModel = partyModel, 
            Inventory = inventory, 
            MovementSpeed = movementSpeed 
        };
    }
}