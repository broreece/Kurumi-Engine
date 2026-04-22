using Engine.Systems.Animation.Map.Base;

namespace Engine.Systems.Animation.Map.Core;

/// <summary>
/// Used to determine the current animation frame of tiles.
/// </summary>
public sealed class MapAnimationManager : ITileFrameAccessor 
{
    // Animation config.
    private readonly int _tileFrameCount;
    private readonly float _tileFrameDuration;

    // Active animation settings.
    private int _tileFrame = 0;
    private float _tileTimer = 0;

    internal MapAnimationManager(int tileFrameCount, float tileFrameDuration) 
    {
        _tileFrameCount = tileFrameCount;
        _tileFrameDuration = tileFrameDuration;
    }

    public void Update(float deltaTime) {
        _tileTimer += deltaTime;

        while (_tileTimer >= _tileFrameDuration) {
            _tileTimer -= _tileFrameDuration;
            _tileFrame++;
        }
    }

    public int GetTileAnimationFrame() => _tileFrame % _tileFrameCount;
}