namespace UI.Interfaces;

/// <summary>
/// Window asset accessor interface, used to access a window assets.
/// </summary>
public interface IWindowAssetAccessor {
    /// <summary>
    /// Getter for a specified font file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified font file name.</returns>
    public string GetFontFileName(int index);

    /// <summary>
    /// Getter for a specified window art file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified window art file name.</returns>
    public string GetWindowArtFileName(int index);

    /// <summary>
    /// Getter for a specified choice selection art file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified choice selection art file name.</returns>
    public string GetChoiceSelectionFileName(int index);
}