// Data.
using Data.Definitions.Actors.Core;

using Data.Models.Maps;

using Data.Runtime.Maps.Base.Controllers.Base;
using Data.Runtime.Maps.Base.Controllers.Core;
using Data.Runtime.Actors.Core;
using Data.Runtime.Spatials;

// Engine.
using Engine.Systems.Navigation.Core;

// Game.
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
        NavigationGrid navigationGrid
    ) 
    {
        Stack<Controller> controllers = [];
        controllers.Push(new SmartTrackingController(target, navigationGrid, actorInfo.TrackingRange) 
        { 
            Interval = actorInfo.MovementSpeed 
        });

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