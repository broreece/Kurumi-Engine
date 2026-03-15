namespace Config.Runtime.Defaults;

/// <summary>
/// The defaults class for the name box.
/// </summary>
public sealed class NameBoxDefaults {
    /// <summary>
    /// Constructor for the name box defaults class.
    /// </summary>
    /// <param name="nameBoxWidth">The default name box width.</param>
    /// <param name="nameBoxHeight">The default name box height.</param>
    /// <param name="nameBoxX">The default name box X location.</param>
    /// <param name="nameBoxY">The default name box Y location.</param>
    public NameBoxDefaults(int nameBoxWidth, int nameBoxHeight, int nameBoxX, int nameBoxY) {
        this.nameBoxWidth = nameBoxWidth;
        this.nameBoxHeight = nameBoxHeight;
        this.nameBoxX = nameBoxX;
        this.nameBoxY = nameBoxY;
    }

    /// <summary>
    /// Getter for the name box width default value.
    /// </summary>
    /// <returns>The name box width default value.</returns>
    public int GetNameBoxWidth() {
        return nameBoxWidth;
    }

    /// <summary>
    /// Getter for the name box height default value.
    /// </summary>
    /// <returns>The name box height default value.</returns>
    public int GetNameBoxHeight() {
        return nameBoxHeight;
    }

    /// <summary>
    /// Getter for the name box X default value.
    /// </summary>
    /// <returns>The name box X default value.</returns>
    public int GetNameBoxX() {
        return nameBoxX;
    }

    /// <summary>
    /// Getter for the name box Y default value.
    /// </summary>
    /// <returns>The name box Y default value.</returns>
    public int GetNameBoxY() {
        return nameBoxY;
    }

    private readonly int nameBoxWidth, nameBoxHeight, nameBoxX, nameBoxY;
}