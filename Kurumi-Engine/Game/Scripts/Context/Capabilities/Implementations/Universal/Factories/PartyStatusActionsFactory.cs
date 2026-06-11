// Data.
using Data.Definitions.Entities.Statuses.Core;

using Data.Runtime.Entities.Statuses.Factories;
using Data.Runtime.Parties.Core;

// Engine.
using Engine.Systems.Statuses.Core;

// Infrastructure.
using Infrastructure.Database.Base;

namespace Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

public sealed class PartyStatusActionsFactory 
{
    private readonly Party _party;

    private readonly Registry<StatusDefinition> _statusRegistry;

    private readonly StatusResolver _statusResolver;
    private readonly StatusFactory _statusFactory;

    public PartyStatusActionsFactory(Party party, Registry<StatusDefinition> statusRegistry)
    {
        _party = party;

        _statusRegistry = statusRegistry;

        _statusResolver = new StatusResolver();
        _statusFactory = new StatusFactory();
    }

    public PartyStatusActions Create()
    {
        return new PartyStatusActions(_party, _statusRegistry, _statusResolver, _statusFactory);
    }
}