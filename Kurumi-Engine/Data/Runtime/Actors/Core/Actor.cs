using Data.Definitions.Actors.Core;
using Data.Models.Maps;
using Data.Runtime.Spatials;
using Data.Runtime.Actors.Controllers.Base;

namespace Data.Runtime.Actors.Core;

public class Actor : IMutablePositionProvider, IFacingPositionProvider, IWalkable 
{
    public required ActorInfo ActorInfo { get; init; }
    public required ActorModel ActorModel { get; init; }

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

    public void AddController(Controller controller) => Controllers.Push(controller);
    
    public void PopController() => Controllers.Pop();

    //IMutablePositionProvider and IFacingPositionProvider functionality.
    public int XLocation 
    {
        get => ActorModel.XLocation;
        set => ActorModel.XLocation = value;
    }

    public int YLocation 
    {
        get => ActorModel.YLocation;
        set => ActorModel.YLocation = value;
    }

    public int Facing 
    {
        get => ActorModel.Facing;
        set => ActorModel.Facing = value;
    }

    public void StartMovement() 
    {
        WalkAnimationFrame = 1;
        MovementProgress = 0;
        IsMoving = true;
    }
}