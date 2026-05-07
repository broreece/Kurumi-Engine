using Data.Runtime.Formations.Base;
using Data.Runtime.Maps.Base;
using Data.Runtime.Maps.Core;
using Infrastructure.Persistance.Base;

namespace Engine.Context.Containers;

public sealed class GameObjects 
{
    public required SaveData SaveData { get; init; }

    public required Map CurrentMap { get; set; }
    public MapChangeRequest? MapChangeRequest { get; set; }

    public BattleStartRequest? BattleStartRequest { get; set; }
}