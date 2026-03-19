namespace Game.Maps.Actors.Base;

/// <summary>
/// Enum used for readability. These values will be hardcoded so just to make it more readable for dev work.
/// </summary>
public enum Behaviour {
    StationaryDoesNotTurn,
    StationaryDoesTurn,
    RandomMovement,
    DumbTracking,
    SmartTracking,
    FollowsPath
}