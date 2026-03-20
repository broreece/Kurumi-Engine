namespace Config.Runtime.Menus;

/// <summary>
/// The config class for the main menu config.
/// </summary>
public sealed class MainMenuConfig {
    /// <summary>
    /// Constructor for the main menu window config.
    /// </summary>
    /// <param name="windowId">The main menu window ID config.</param>
    /// <param name="choiceBoxId">The main menu choice box ID config.</param>
    /// <param name="fontId">The main menu font ID config.</param>
    /// <param name="fontSize">The main menu font size config.</param>
    /// <param name="selectionWindowWidth">The selection window width config.</param>
    /// <param name="selectionWindowHeight">The selection window height config.</param>
    /// <param name="selectionWindowX">The selection window X config.</param>
    /// <param name="selectionWindowY">The selection window Y config.</param>
    /// <param name="selectionWindowSpacing">The selection window spacing config.</param>
    /// <param name="infoWindowWidth">The info window width config.</param>
    /// <param name="infoWindowHeight">The info window height config.</param>
    /// <param name="infoWindowX">The info window X config.</param>
    /// <param name="infoWindowY">The info window Y config.</param>
    /// <param name="partyWindowWidth">The party window width config.</param>
    /// <param name="partyWindowHeight">The party window height config.</param>
    /// <param name="partyWindowX">The party window X config.</param>
    /// <param name="partyWindowY">The party window Y config.</param>
    public MainMenuConfig(int windowId, int choiceBoxId, int fontId, int fontSize, int selectionWindowWidth, 
        int selectionWindowHeight, int selectionWindowX, int selectionWindowY, int selectionWindowSpacing, 
        int infoWindowWidth, int infoWindowHeight, int infoWindowX, int infoWindowY, int partyWindowWidth, 
        int partyWindowHeight, int partyWindowX, int partyWindowY) {
        this.windowId = windowId;
        this.choiceBoxId = choiceBoxId;
        this.fontId = fontId;
        this.fontSize = fontSize;
        this.selectionWindowWidth = selectionWindowWidth;
        this.selectionWindowHeight = selectionWindowHeight;
        this.selectionWindowX = selectionWindowX;
        this.selectionWindowY = selectionWindowY;
        this.selectionWindowSpacing = selectionWindowSpacing;
        this.infoWindowWidth = infoWindowWidth;
        this.infoWindowHeight = infoWindowHeight;
        this.infoWindowX = infoWindowX;
        this.infoWindowY = infoWindowY;
        this.partyWindowWidth = partyWindowWidth;
        this.partyWindowHeight = partyWindowHeight;
        this.partyWindowX = partyWindowX;
        this.partyWindowY = partyWindowY;
    }

    /// <summary>
    /// Getter for the main menu window ID config.
    /// </summary>
    /// <returns>The main menu window ID config.</returns>
    public int GetWindowId() {
        return windowId;
    }

    /// <summary>
    /// Getter for the main menu choice box ID config.
    /// </summary>
    /// <returns>The main menu choice box ID config.</returns>
    public int GetChoiceBoxId() {
        return choiceBoxId;
    }

    /// <summary>
    /// Getter for the font ID config.
    /// </summary>
    /// <returns>The font ID config.</returns>
    public int GetFontId() {
        return fontId;
    }

    /// <summary>
    /// Getter for the font size config.
    /// </summary>
    /// <returns>The font size config.</returns>
    public int GetFontSize() {
        return fontSize;
    }

    /// <summary>
    /// Getter for the selection window width config.
    /// </summary>
    /// <returns>The selection window width config.</returns>
    public int GetSelectionWindowWidth() {
        return selectionWindowWidth;
    }

    /// <summary>
    /// Getter for the selection window height config.
    /// </summary>
    /// <returns>The selection window height config.</returns>
    public int GetSelectionWindowHeight() {
        return selectionWindowHeight;
    }

    /// <summary>
    /// Getter for the selection window X config.
    /// </summary>
    /// <returns>The selection window X config.</returns>
    public int GetSelectionWindowX() {
        return selectionWindowX;
    }

    /// <summary>
    /// Getter for the selection window Y config.
    /// </summary>
    /// <returns>The selection window Y config.</returns>
    public int GetSelectionWindowY() {
        return selectionWindowY;
    }

    /// <summary>
    /// Getter for the selection window spacing config.
    /// </summary>
    /// <returns>The selection window spacing config.</returns>
    public int GetSelectionWindowSpacing() {
        return selectionWindowSpacing;
    }

    /// <summary>
    /// Getter for the info window width config.
    /// </summary>
    /// <returns>The info window width config.</returns>
    public int GetInfoWindowWidth() {
        return infoWindowWidth;
    }

    /// <summary>
    /// Getter for the info window height config.
    /// </summary>
    /// <returns>The info window height config.</returns>
    public int GetInfoWindowHeight() {
        return infoWindowHeight;
    }

    /// <summary>
    /// Getter for the info window X config.
    /// </summary>
    /// <returns>The info window X config.</returns>
    public int GetInfoWindowX() {
        return infoWindowX;
    }

    /// <summary>
    /// Getter for the info window Y config.
    /// </summary>
    /// <returns>The info window Y config.</returns>
    public int GetInfoWindowY() {
        return infoWindowY;
    }

    /// <summary>
    /// Getter for the party window width config.
    /// </summary>
    /// <returns>The party window width config.</returns>
    public int GetPartyWindowWidth() {
        return partyWindowWidth;
    }

    /// <summary>
    /// Getter for the party window height config.
    /// </summary>
    /// <returns>The party window height config.</returns>
    public int GetPartyWindowHeight() {
        return partyWindowHeight;
    }

    /// <summary>
    /// Getter for the party window X config.
    /// </summary>
    /// <returns>The party window X config.</returns>
    public int GetPartyWindowX() {
        return partyWindowX;
    }

    /// <summary>
    /// Getter for the party window Y config.
    /// </summary>
    /// <returns>The party window Y config.</returns>
    public int GetPartyWindowY() {
        return partyWindowY;
    }

    private readonly int windowId, choiceBoxId, fontId, fontSize, selectionWindowWidth, selectionWindowHeight, 
        selectionWindowX, selectionWindowY, selectionWindowSpacing, infoWindowWidth, infoWindowHeight, infoWindowX, 
        infoWindowY, partyWindowWidth, partyWindowHeight, partyWindowX, partyWindowY;
}