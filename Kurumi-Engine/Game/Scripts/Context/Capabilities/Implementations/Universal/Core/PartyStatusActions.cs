// Data.
using Data.Definitions.Entities.Statuses.Core;

using Data.Runtime.Entities.Statuses.Factories;
using Data.Runtime.Parties.Core;

// Engine.
using Engine.Systems.Statuses.Core;

// Game.
using Game.Scripts.Context.Capabilities.Interfaces.Universal;

// Infrastructure.
using Infrastructure.Database.Base;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

public sealed class PartyStatusActions : IPartyStatusActions 
{
    private readonly Party _party;

    private readonly Registry<StatusDefinition> _statusRegistry;

    private readonly StatusResolver _statusResolver;
    private readonly StatusFactory _statusFactory;

    public PartyStatusActions(Party party, Registry<StatusDefinition> statusRegistry)
    {
        _party = party;

        _statusRegistry = statusRegistry;

        _statusResolver = new StatusResolver();
        _statusFactory = new StatusFactory();
    }

    public void AddStatusToParty(int statusId) 
    {
        var status = _statusFactory.Create(_statusRegistry.Get(statusId));
        foreach (var character in _party.Characters)
        {
            if (character != null)
            {
                _statusResolver.TryApplyStatus(character, status);
            }
        }
    }
}