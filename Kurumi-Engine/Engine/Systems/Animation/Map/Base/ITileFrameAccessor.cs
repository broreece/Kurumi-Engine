namespace Engine.Systems.Animation.Map.Base;

/// <summary>
/// Allows access of the current frame of the tile animation.
/// </summary>
public interface ITileFrameAccessor 
{
    public int GetTileAnimationFrame();
}