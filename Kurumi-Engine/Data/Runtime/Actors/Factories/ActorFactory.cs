using Data.Definitions.Actors.Core;
using Data.Models.Maps;
using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Actors.Core;

namespace Data.Runtime.Actors.Factories;

/// <summary>
/// The default actor factory used for actors with no movement.
/// </summary>
public sealed class ActorFactory 
{
    public Actor Create(ActorInfo actorInfo, ActorModel actorModel) 
    {
        var controllers = new Stack<Controller>();
        return new Actor{ActorInfo = actorInfo, ActorModel = actorModel, Controllers = controllers};
    }
}