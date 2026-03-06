namespace Save.Interfaces;

/// <summary>
/// Interface used to be able to access an ID value.
/// </summary>
public interface IIDAccessor {
    /// <summary>
    /// Getter for the objects's id.
    /// </summary>
    /// <returns>The objects's id.</returns>
    public int GetId();
}