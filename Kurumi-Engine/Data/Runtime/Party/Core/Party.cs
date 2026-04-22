using Data.Models.Party;
using Data.Runtime.Spatials;

namespace Data.Runtime.Party.Core;

public sealed class Party : IMutablePositionProvider, IFacingPositionProvider, IWalkable 
{
    public required PartyModel PartyModel { get; init; }
    
    public required Dictionary<int, int> Inventory { get; init; }

    // Walk animation variables.
    public int LastX { get; set; }
    public int LastY { get; set; }

    public bool IsMoving { get; set; } = false;
    public bool MovingLastFrame { get; set; } = false;

    public int WalkAnimationFrame { get; set; } = 0;

    public float AnimationTimer { get; set; } = 0;
    public float MovementProgress { get; set; } = 1;

    //IMutablePositionProvider and IFacingPositionProvider functionality.
    public int XLocation 
    {
        get => PartyModel.XLocation;
        set => PartyModel.XLocation = value;
    }

    public int YLocation 
    {
        get => PartyModel.YLocation;
        set => PartyModel.YLocation = value;
    }

    public int Facing 
    {
        get => PartyModel.Facing;
        set => PartyModel.Facing = value;
    }

    /// <summary>
    /// Function that starts the movement of the party.
    /// </summary>
    public void StartMovement() 
    {
        WalkAnimationFrame = 1;
        MovementProgress = 0;
        IsMoving = true;
        MovingLastFrame = true;
    }
}