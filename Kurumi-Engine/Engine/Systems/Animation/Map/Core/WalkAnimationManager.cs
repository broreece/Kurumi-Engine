using Data.Runtime.Actors.Core;
using Data.Runtime.Party.Core;
using Data.Runtime.Spatials;

namespace Engine.Systems.Animation.Map.Core;

public sealed class WalkAnimationManager 
{
    private readonly IReadOnlyList<Actor> _actors;

    private readonly Party _party;

    private readonly int _walkAnimationFrames;
    private readonly float _partyWalkAnimationLength;

    internal WalkAnimationManager(
        IReadOnlyList<Actor> actors, 
        Party party, 
        int walkAnimationFrames, 
        float partyWalkAnimationLength) 
    {
        _actors = actors;
        _party = party;
        _walkAnimationFrames = walkAnimationFrames;
        _partyWalkAnimationLength = partyWalkAnimationLength;
    }

    public void Update(float deltaTime) 
    {
        // Update actors.
        foreach (var actor in _actors) 
        {
            UpdateWalkableEntity(actor, actor.ActorInfo.MovementSpeed, deltaTime);
        }

        // Update party.
        UpdateWalkableEntity(_party, _partyWalkAnimationLength, deltaTime);
    }

    /// <summary>
    /// Helper function that updates a single walkable entity.
    /// </summary>
    /// <param name="walkableEntity">The walkable entity.</param>
    /// <param name="walkAnimationLength">The length of the walk animation.</param>
    /// <param name="deltaTime">The time passed since the last update.</param>
    private void UpdateWalkableEntity(IWalkable walkableEntity, float walkAnimationLength, float deltaTime) 
    {
        if (walkableEntity.IsMoving) 
        {
            float frameDuration = walkAnimationLength / _walkAnimationFrames;
            walkableEntity.AnimationTimer += deltaTime;
            walkableEntity.MovementProgress += deltaTime / frameDuration;
 
            while (walkableEntity.AnimationTimer >= frameDuration) 
            {
                // If current walk frame is final walk frame end the walk animation.
                if (walkableEntity.WalkAnimationFrame == _walkAnimationFrames) 
                {
                    // Set this to 1 in case it exceed 1 value.
                    walkableEntity.MovementProgress = 1f;

                    walkableEntity.WalkAnimationFrame = 0;
                    walkableEntity.IsMoving = false;
                }
                else 
                {
                    walkableEntity.WalkAnimationFrame ++;
                }
                walkableEntity.AnimationTimer = 0;
            }
        }
        else 
        {
            walkableEntity.AnimationTimer = 0;
        }
    }
}