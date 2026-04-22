namespace Data.Definitions.Actors.Base;

public enum ActorBehaviour 
{
    StationaryDoesNotTurn = 0,
    StationaryDoesTurn = 1,
    RandomMovement = 2,
    DumbTracking = 3,
    SmartTracking = 4,
    FollowsPath = 5
}