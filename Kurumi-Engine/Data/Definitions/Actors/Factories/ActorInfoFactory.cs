// Data.
using Data.Definitions.Actors.Core;

namespace Data.Definitions.Actors.Factories;

public sealed class ActorInfoFactory 
{
    public ActorInfo Create(
        int id, 
        int spriteId, 
        int behaviour, 
        int trackingRange, 
        float movementSpeed, 
        string? scriptName, 
        bool belowParty, 
        bool seeThrough, 
        bool onTouch, 
        bool auto, 
        bool onAction, 
        bool onFind, 
        IReadOnlyList<int> path
    ) 
    {
        return new ActorInfo() 
        {
            Id = id, 
            SpriteId = spriteId, 
            Behaviour = behaviour, 
            TrackingRange = trackingRange, 
            MovementSpeed = movementSpeed, 
            ScriptName = scriptName, 
            BelowParty = belowParty, 
            SeeThrough = seeThrough, 
            OnTouch = onTouch, 
            Auto = auto, 
            OnAction = onAction, 
            OnFind = onFind, 
            Path = path
        };
    }
}