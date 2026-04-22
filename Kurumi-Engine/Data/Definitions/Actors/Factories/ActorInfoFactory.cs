using Data.Definitions.Actors.Core;

namespace Data.Definitions.Actors.Factories;

public sealed class ActorInfoFactory 
{
    public ActorInfo Create(int id, 
        int spriteId, 
        int behaviour, 
        int movementSpeed, 
        int trackingRange, 
        string? scriptName, 
        bool belowParty, 
        bool passable, 
        bool onTouch, 
        bool auto, 
        bool onAction, 
        bool onFind, 
        IReadOnlyList<int> path) 
    {
        return new ActorInfo() 
        {
            Id = id, 
            SpriteId = spriteId, 
            Behaviour = behaviour, 
            MovementSpeed = movementSpeed, 
            TrackingRange = trackingRange, 
            ScriptName = scriptName, 
            BelowParty = belowParty, 
            Passable = passable,
            OnTouch = onTouch, 
            Auto = auto, 
            OnAction = onAction, 
            OnFind = onFind, 
            Path = path
        };
    }
}