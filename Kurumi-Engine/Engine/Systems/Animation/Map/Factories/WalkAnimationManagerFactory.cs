using Data.Runtime.Actors.Core;
using Data.Runtime.Party.Core;
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

    public WalkAnimationManager Create(IReadOnlyList<Actor> actors, Party party)
    {
        return new WalkAnimationManager(actors, party, _walkAnimationFrames, _partyWalkAnimationLength);
    }
}