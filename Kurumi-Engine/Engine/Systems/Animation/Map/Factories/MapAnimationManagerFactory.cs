using Engine.Systems.Animation.Map.Core;

namespace Engine.Systems.Animation.Map.Factories;

public sealed class MapAnimationManagerFactory 
{
    private readonly int _tileFrameCount;
    private readonly float _tileFrameDuration;

    public MapAnimationManagerFactory(int tileFrameCount, float tileFrameDuration) 
    {
        _tileFrameCount = tileFrameCount;
        _tileFrameDuration = tileFrameDuration;
    }

    public MapAnimationManager Create() 
    {
        return new MapAnimationManager(_tileFrameCount, _tileFrameDuration);
    }
}