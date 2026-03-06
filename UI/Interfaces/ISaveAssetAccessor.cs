namespace UI.Interfaces;

/// <summary>
/// Save asset accessor interface, used to access save assets.
/// </summary>
public interface ISaveAssetAccessor : IWindowAssetAccessor {
    /// <summary>
    /// Getter for a specified character field sprite sheet file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified character field sprite sheet file name.</returns>
    public string GetCharacterFieldSheetFileName(int index);
}