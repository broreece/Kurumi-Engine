namespace Game.Map.Actors.Base;

/// <summary>
/// Enum used for readability. These values will be hardcoded so just to make it more readable for dev work.
/// </summary>
public enum Behaviour {
    StationaryDoesNotTurn = 0,
    StationaryDoesTurn = 1,
    RandomMovement = 2,
    DumbTracking = 3,
    SmartTracking = 4,
    FollowsPath = 5
}