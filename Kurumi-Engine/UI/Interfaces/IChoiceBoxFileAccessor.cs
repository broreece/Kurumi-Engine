namespace UI.Interfaces;

/// <summary>
/// Choice box file accessor interface, used to access the choice box file width and height.
/// </summary>
public interface IChoiceBoxFileAccessor {
    /// <summary>
    /// Getter for the choice box file width stored in the window config.
    /// </summary>
    /// <returns>The choice box file width in config.</returns>
    public int GetChoiceSelectionFileWidth();

    /// <summary>
    /// Getter for the choice box file height stored in the window config.
    /// </summary>
    /// <returns>The choice box file height in config.</returns>
    public int GetChoiceSelectionFileHeight();
}
