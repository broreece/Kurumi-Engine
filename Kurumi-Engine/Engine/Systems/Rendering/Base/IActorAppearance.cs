namespace Engine.Systems.Rendering.Base;

public interface IActorAppearance
{
    // Location properties.
    public int XLocation { get; }

    public int YLocation { get; }

    public int SpriteState { get; }

    // Walk animation properties.
    public int WalkAnimationFrame { get; }

    public int LastX { get; }

    public int LastY { get; }
    
    public float MovementProgress { get; }

    public bool BelowParty { get; }

    // Actor information properties.
    public int SpriteId { get; }
}