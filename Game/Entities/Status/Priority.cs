namespace Game.Entities.Status;

/// <summary>
/// Enum used for status priority.
/// </summary>
public enum Priority {
    EraseIfOtherIsAdded,
    CanStackWithOthers,
    EraseAllOthersWhenAdded,
    Fainted
}