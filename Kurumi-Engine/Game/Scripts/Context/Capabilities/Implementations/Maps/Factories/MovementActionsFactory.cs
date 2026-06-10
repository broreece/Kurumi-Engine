// Data.
using Data.Runtime.Maps.Core;
using Data.Runtime.Parties.Core;

// Game.
using Game.Scripts.Context.Capabilities.Implementations.Maps.Core;

namespace Game.Scripts.Context.Capabilities.Implementations.Maps.Factories;

public sealed class MovementActionsFactory
{
    private readonly Party _party;

    public MovementActionsFactory(Party party)
    {
        _party = party;
    }

    public MovementActions Create(Map map)
    {
        return new MovementActions(_party, map);
    }
}