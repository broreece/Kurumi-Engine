using Data.Definitions.Actors.Core;
using Data.Models.Maps;
using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Actors.Core;

using Game.Scripts.Library;

namespace Data.Runtime.Actors.Factories;

/// <summary>
/// The default actor factory used for actors with no movement.
/// </summary>
public sealed class ActorFactory 
{
    private readonly ScriptLibrary _scriptLibrary;

    public ActorFactory(ScriptLibrary scriptLibrary)
    {
        _scriptLibrary = scriptLibrary;
    }

    public Actor Create(ActorInfo actorInfo, ActorModel actorModel) 
    {
        var controllers = new Stack<Controller>();
        if (actorInfo.ScriptName == null) 
        {
            return new Actor() 
            {
                ActorInfo = actorInfo, 
                ActorModel = actorModel, 
                Controllers = controllers,
                Script = null
            };
        }
        return new Actor() 
        {
            ActorInfo = actorInfo, 
            ActorModel = actorModel, 
            Controllers = controllers,
            Script = _scriptLibrary.GetMapScript(actorInfo.ScriptName)
        };
    }
}