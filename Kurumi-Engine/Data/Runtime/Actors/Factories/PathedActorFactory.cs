using Data.Definitions.Actors.Core;
using Data.Models.Maps;
using Data.Runtime.Actors.Controllers.Base;
using Data.Runtime.Actors.Controllers.Core;
using Data.Runtime.Actors.Core;

using Game.Scripts.Library;

namespace Data.Runtime.Actors.Factories;

public sealed class PathedActorFactory 
{
    private readonly ScriptLibrary _scriptLibrary;

    public PathedActorFactory(ScriptLibrary scriptLibrary)
    {
        _scriptLibrary = scriptLibrary;
    }

    public Actor Create(ActorInfo actorInfo, ActorModel actorModel) 
    {
        Stack<Controller> controllers = [];
        controllers.Push(new PathedController(canFinish: false, actorInfo.Path) {Interval = actorInfo.MovementSpeed});
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