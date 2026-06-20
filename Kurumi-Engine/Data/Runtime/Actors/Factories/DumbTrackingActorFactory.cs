// Data.
using Data.Definitions.Actors.Core;

using Data.Models.Maps.Core;

using Data.Runtime.Maps.Base.Controllers.Base;
using Data.Runtime.Maps.Base.Controllers.Core;
using Data.Runtime.Actors.Core;
using Data.Runtime.Spatials;

// Game.
using Game.Scripts.Library;

namespace Data.Runtime.Actors.Factories;

public sealed class DumbTrackingActorFactory 
{
    private readonly ScriptLibrary _scriptLibrary;

    public DumbTrackingActorFactory(ScriptLibrary scriptLibrary)
    {
        _scriptLibrary = scriptLibrary;
    }

    public Actor Create(ActorInfo actorInfo, ActorModel actorModel, IPositionProvider target) 
    {
        Stack<Controller> controllers = [];
        controllers.Push(new DumbTrackingController(target, actorInfo.TrackingRange) 
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