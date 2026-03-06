namespace Config.Runtime.Defaults;

/// <summary>
/// The defaults class for the choice box.
/// </summary>
public sealed class ChoiceBoxDefaults {
    /// <summary>
    /// Constructor for the choice box defaults class.
    /// </summary>
    /// <param name="choiceBoxWidth">The default choice box width.</param>
    /// <param name="choiceBoxHeight">The default choice box height.</param>
    /// <param name="choiceBoxX">The default choice box X location.</param>
    /// <param name="choiceBoxY">The default choice box Y location.</param>
    public ChoiceBoxDefaults(int choiceBoxWidth, int choiceBoxHeight, int choiceBoxX, int choiceBoxY) {
        this.choiceBoxWidth = choiceBoxWidth;
        this.choiceBoxHeight = choiceBoxHeight;
        this.choiceBoxX = choiceBoxX;
        this.choiceBoxY = choiceBoxY;
    }

    /// <summary>
    /// Getter for the choice box width default value.
    /// </summary>
    /// <returns>The choice box width default value.</returns>
    public int GetChoiceBoxWidth() {
        return choiceBoxWidth;
    }

    /// <summary>
    /// Getter for the choice box height default value.
    /// </summary>
    /// <returns>The choice box height default value.</returns>
    public int GetChoiceBoxHeight() {
        return choiceBoxHeight;
    }

    /// <summary>
    /// Getter for the choice box X default value.
    /// </summary>
    /// <returns>The choice box X default value.</returns>
    public int GetChoiceBoxX() {
        return choiceBoxX;
    }

    /// <summary>
    /// Getter for the choice box Y default value.
    /// </summary>
    /// <returns>The choice box Y default value.</returns>
    public int GetChoiceBoxY() {
        return choiceBoxY;
    }

    private readonly int choiceBoxWidth, choiceBoxHeight, choiceBoxX, choiceBoxY;
}