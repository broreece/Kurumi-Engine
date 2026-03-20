namespace Config.Runtime.Battle;

/// <summary>
/// The config class for the battle window config.
/// </summary>
public sealed class BattleWindowConfig {
    /// <summary>
    /// The constructor function for the battle window config.
    /// </summary>
    /// <param name="windowId">The window ID of the battle scene config.</param>
    /// <param name="choiceBoxId">The choice box ID of the battle scene config.</param>
    /// <param name="fontId">The font ID of the battle scene config.</param>
    /// <param name="fontSize">The font size of the battle scene config.</param>
    /// <param name="infoWindowWidth">The info window width of the battle scene config.</param>
    /// <param name="infoWindowHeight">The info window height of the battle scene config.</param>
    /// <param name="infoWindowX">The info window X location of the battle scene config.</param>
    /// <param name="infoWindowY">The info window Y location of the battle scene config.</param>
    /// <param name="selectionWindowWidth">The selection window width of the battle scene config.</param>
    /// <param name="selectionWindowHeight">The selection window width of the battle scene config.</param>
    /// <param name="selectionWindowX">The selection window X location of the battle scene config.</param>
    /// <param name="selectionWindowY">The selection window Y location of the battle scene config.</param>
    /// <param name="partyX">The party X location of the battle scene config.</param>
    /// <param name="partyY">The party Y location pf the battle scene config.</param>
    public BattleWindowConfig(int windowId, int choiceBoxId, int fontId, int fontSize, int infoWindowWidth, 
        int infoWindowHeight, int infoWindowX, int infoWindowY, int selectionWindowWidth, int selectionWindowHeight, 
        int selectionWindowX, int selectionWindowY,
        int partyX, int partyY) {
        this.windowId = windowId;
        this.choiceBoxId = choiceBoxId;
        this.fontId = fontId;
        this.fontSize = fontSize;
        this.infoWindowWidth = infoWindowWidth;
        this.infoWindowHeight = infoWindowHeight;
        this.infoWindowX = infoWindowX;
        this.infoWindowY = infoWindowY;
        this.selectionWindowWidth = selectionWindowWidth;
        this.selectionWindowHeight = selectionWindowHeight;
        this.selectionWindowX = selectionWindowX;
        this.selectionWindowY = selectionWindowY;
        this.partyX = partyX;
        this.partyY = partyY;
    }

    /// <summary>
    /// Getter for the battle scene window ID config.
    /// </summary>
    /// <returns>The battle scene window ID config.</returns>
    public int GetWindowId() {
        return windowId;
    }

    /// <summary>
    /// Getter for the battle scene choice box ID config.
    /// </summary>
    /// <returns>The battle scene choice box ID config.</returns>
    public int GetChoiceBoxId() {
        return choiceBoxId;
    }

    /// <summary>
    /// Getter for the battle scene window font config.
    /// </summary>
    /// <returns>The battle scene window font config.</returns>
    public int GetFontId() {
        return fontId;
    }

    /// <summary>
    /// Getter for the battle scene window font size config.
    /// </summary>
    /// <returns>The battle scene window font size config.</returns>
    public int GetFontSize() {
        return fontSize;
    }

    /// <summary>
    /// Getter for the battle scene info window width config.
    /// </summary>
    /// <returns>The battle scene info window width config.</returns>
    public int GetInfoWindowWidth() {
        return infoWindowWidth;
    }

    /// <summary>
    /// Getter for the battle scene info window height config.
    /// </summary>
    /// <returns>The battle scene info window height config.</returns>
    public int GetInfoWindowHeight() {
        return infoWindowHeight;
    }

    /// <summary>
    /// Getter for the battle scene info window X config.
    /// </summary>
    /// <returns>The battle scene info window X config.</returns>
    public int GetInfoWindowX() {
        return infoWindowX;
    }

    /// <summary>
    /// Getter for the battle scene info window Y config.
    /// </summary>
    /// <returns>The battle scene info window Y config.</returns>
    public int GetInfoWindowY() {
        return infoWindowY;
    }

    /// <summary>
    /// Getter for the battle scene selection window width config.
    /// </summary>
    /// <returns>The battle scene selection window width config.</returns>
    public int GetSelectionWindowWidth() {
        return selectionWindowWidth;
    }

    /// <summary>
    /// Getter for the battle scene info window height config.
    /// </summary>
    /// <returns>The battle scene info window height config.</returns>
    public int GetSelectionWindowHeight() {
        return selectionWindowHeight;
    }

    /// <summary>
    /// Getter for the battle scene info selection X config.
    /// </summary>
    /// <returns>The battle scene info selection X config.</returns>
    public int GetSelectionWindowX() {
        return selectionWindowX;
    }

    /// <summary>
    /// Getter for the battle scene selection window Y config.
    /// </summary>
    /// <returns>The battle scene selection window Y config.</returns>
    public int GetSelectionWindowY() {
        return selectionWindowY;
    }

    /// <summary>
    /// Getter for the battle scene info party X config.
    /// </summary>
    /// <returns>The battle scene info party X config.</returns>
    public int GetPartyX() {
        return partyX;
    }

    /// <summary>
    /// Getter for the battle scene info party Y config.
    /// </summary>
    /// <returns>The battle scene info party Y config.</returns>
    public int GetPartyY() {
        return partyY;
    }

    private readonly int windowId, choiceBoxId, fontId, fontSize, infoWindowWidth, infoWindowHeight, infoWindowX, 
        infoWindowY, selectionWindowWidth, selectionWindowHeight, selectionWindowX, selectionWindowY, partyX, partyY;
}