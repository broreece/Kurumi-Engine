using Data.Definitions.Actors.Core;
using Data.Models.Maps;
using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Actors.Controllers.Core;
using Data.Runtime.Actors.Core;
using Data.Runtime.Spatials;

using Engine.Systems.Navigation.Core;

using Game.Scripts.Library;

namespace Data.Runtime.Actors.Factories;

public sealed class SmartTrackingActorFactory 
{
    private readonly ScriptLibrary _scriptLibrary;

    public SmartTrackingActorFactory(ScriptLibrary scriptLibrary)
    {
        _scriptLibrary = scriptLibrary;
    }

    public Actor Create(
        ActorInfo actorInfo, 
        ActorModel actorModel, 
        IPositionProvider target, 
        NavigationGrid navigationGrid) 
    {
        Stack<Controller> controllers = [];
        controllers.Push(new SmartTrackingController(target, navigationGrid) {Interval = actorInfo.MovementSpeed});
        if (actorInfo.ScriptName == null)
        {
            return new Actor(actorInfo, actorModel) 
            {
                Controllers = controllers,
                Script = null
            };
        }
        return new Actor(actorInfo, actorModel) 
        {
            Controllers = controllers,
            Script = _scriptLibrary.GetMapScript(actorInfo.ScriptName)
        };
    }
}