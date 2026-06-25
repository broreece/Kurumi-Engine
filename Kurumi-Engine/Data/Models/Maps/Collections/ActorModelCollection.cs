// Data.
using Data.Models.Maps.Core;

namespace Data.Models.Maps.Collections;

public sealed class ActorModelCollection 
{
    public required Dictionary<string, IReadOnlyList<ActorModel>> Actors { get; set; }

    public IReadOnlyList<ActorModel> GetActorsOnMap(string mapName)
    {
        if (Actors.TryGetValue(mapName, out var actors))
        {
            return actors;
        }
        return [];
    }
}