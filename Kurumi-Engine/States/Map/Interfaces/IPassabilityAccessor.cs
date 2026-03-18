namespace States.Map.Interfaces;

/// <summary>
/// The public passability accessor interface. Used to check if an object is passable or not.
/// </summary>
public interface IPassabilityAccessor {
    /// <summary>
    /// Returns if the object is passable.
    /// </summary>
    /// <returns>True: The object can be walked over.
    /// False: The object is not passable.</returns>
    public bool IsPassable();
}