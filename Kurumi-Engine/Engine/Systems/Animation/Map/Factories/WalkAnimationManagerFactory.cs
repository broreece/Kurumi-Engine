// Data.
using Data.Runtime.Parties.Core;
using Data.Runtime.Spatials;

// Engine.
using Engine.Systems.Animation.Map.Core;

namespace Engine.Systems.Animation.Map.Factories;

public sealed class WalkAnimationManagerFactory
{
    private readonly int _walkAnimationFrames;
    private readonly float _partyWalkAnimationLength;
    
    public WalkAnimationManagerFactory(int walkAnimationFrames, float partyWalkAnimationLength)
    {
        _walkAnimationFrames = walkAnimationFrames;
        _partyWalkAnimationLength = partyWalkAnimationLength;
    }

    public WalkAnimationManager Create(IReadOnlyList<IWalkable> walkableEntities, Party party)
    {
        return new WalkAnimationManager(walkableEntities, party, _walkAnimationFrames, _partyWalkAnimationLength);
    }
}