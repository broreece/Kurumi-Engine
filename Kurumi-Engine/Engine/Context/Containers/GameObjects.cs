// Data.
using Data.Runtime.Formations.Base;
using Data.Runtime.Maps.Base.Change;
using Data.Runtime.Maps.Core;
using Data.Runtime.Parties.Core;

// Infrastructure.
using Infrastructure.Persistance.Base;

namespace Engine.Context.Containers;

public sealed class GameObjects 
{
    // Save data.
    public required SaveData SaveData { get; init; }

    // Party.
    public required Party Party { get; init; }

    // Current map and map change requests.
    public required Map CurrentMap { get; set; }
    public MapChangeRequest? MapChangeRequest { get; set; }

    // Battle start requests and the battle end request.
    public BattleStartRequest? BattleStartRequest { get; set; }
    public BattleEndRequest? BattleEndRequest { get; set; }
}