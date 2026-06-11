// Data.
using Data.Runtime.Parties.Core;
using Data.Runtime.Spatials;

// Engine.
using Engine.Systems.Animation.Map.Core;

namespace Engine.Systems.Animation.Map.Factories;

public sealed class WalkAnimationManagerFactory
{
    private readonly int _walkAnimationFrames;
    
    public WalkAnimationManagerFactory(int walkAnimationFrames)
    {
        _walkAnimationFrames = walkAnimationFrames;
    }

    public WalkAnimationManager Create(IReadOnlyList<IWalkable> walkableEntities, Party party)
    {
        return new WalkAnimationManager(walkableEntities, party, _walkAnimationFrames);
    }
}