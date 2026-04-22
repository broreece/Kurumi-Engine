using Data.Definitions.Actors.Core;
using Data.Models.Maps;
using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Actors.Controllers.Core;
using Data.Runtime.Actors.Core;
using Data.Runtime.Spatials;
using Engine.Systems.Navigation.Core;

namespace Data.Runtime.Actors.Factories;

public sealed class SmartTrackingActorFactory 
{
    public Actor Create(ActorInfo actorInfo, ActorModel actorModel, IPositionProvider target, 
        NavigationGrid navigationGrid) 
    {
        Stack<Controller> controllers = [];
        controllers.Push(new SmartTrackingController(target, navigationGrid) {Interval = actorInfo.MovementSpeed});
        return new Actor{ActorInfo = actorInfo, ActorModel = actorModel, Controllers = controllers};
    }
}