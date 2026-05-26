namespace Engine.Systems.Rendering.Base;

public interface IActorAppearance
{
    // Location variables.
    public int XLocation { get; }

    public int YLocation { get; }

    public int Facing { get; }

    // Walk animation variables.
    public int WalkAnimationFrame { get; }

    public int LastX { get; }

    public int LastY { get; }
    
    public float MovementProgress { get; }

    // Actor information functions.
    public int GetSpriteId();

    public bool IsBelowParty();
}