using Data.Definitions.Actors.Core;
using Data.Models.Maps;
using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Actors.Controllers.Core;
using Data.Runtime.Actors.Core;

namespace Data.Runtime.Actors.Factories;

public sealed class RandomActorFactory 
{
    public Actor Create(ActorInfo actorInfo, ActorModel actorModel) 
    {
        Stack<Controller> controllers = [];
        controllers.Push(new RandomController() {Interval = actorInfo.MovementSpeed});
        return new Actor{ActorInfo = actorInfo, ActorModel = actorModel, Controllers = controllers};
    }
}