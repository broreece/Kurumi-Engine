namespace Data.Runtime.Spatials;

/// <summary>
/// The interface used for runtime objects that can walk and have a related animation to walking.
/// </summary>
public interface IWalkable 
{
    public int WalkAnimationFrame { get; set; }

    public float AnimationTimer { get; set; }

    public bool IsMoving { get; set; }

    public float MovementProgress { get; set; }

    public int LastX { get; }

    public int LastY { get; }
}