using Data.Definitions.Actors.Core;
using Data.Models.Maps;
using Data.Runtime.Spatials;
using Data.Runtime.Actors.Controllers.Base;

using Game.Scripts.Core;

namespace Data.Runtime.Actors.Core;

public class Actor : IMutablePositionProvider, IFacingPositionProvider, IWalkable 
{
    private readonly ActorInfo _actorInfo;
    private readonly ActorModel _actorModel;

    public required Script? Script { get; init; }

    public required Stack<Controller> Controllers { get; init; }

    // Walk animation variables.
    public bool IsMoving { get; set; } = false;
    public int WalkAnimationFrame { get; set; } = 0;
    public int LastX { get; set; }
    public int LastY { get; set; }
    public float AnimationTimer { get; set; } = 0;
    public float MovementProgress { get; set; } = 1;

    // Force movement variables.
    public bool MaintainFacing { get; set; } = false;

    public int SpriteId => _actorInfo.SpriteId;

    public int Behaviour => _actorInfo.Behaviour;

    public int MovementSpeed => _actorInfo.MovementSpeed;

    public int TrackingRange => _actorInfo.TrackingRange;

    public bool BelowParty => _actorInfo.BelowParty;

    public bool Passable => _actorInfo.Passable;

    public bool SeeThrough => _actorInfo.SeeThrough;

    public bool OnTouch => _actorInfo.OnTouch;

    public bool OnAction => _actorInfo.OnAction;

    public bool OnFind => _actorInfo.OnFind;

    //IMutablePositionProvider and IFacingPositionProvider functionality.
    public int XLocation 
    {
        get => _actorModel.XLocation;
        set => _actorModel.XLocation = value;
    }

    public int YLocation 
    {
        get => _actorModel.YLocation;
        set => _actorModel.YLocation = value;
    }

    public int Facing 
    {
        get => _actorModel.Facing;
        set => _actorModel.Facing = value;
    }

    internal Actor(ActorInfo actorInfo, ActorModel actorModel)
    {
        _actorInfo = actorInfo;
        _actorModel = actorModel;
    }

    public void AddController(Controller controller) => Controllers.Push(controller);
    
    public void PopController() => Controllers.Pop();

    public void StartMovement() 
    {
        WalkAnimationFrame = 1;
        MovementProgress = 0;
        IsMoving = true;
    }
}