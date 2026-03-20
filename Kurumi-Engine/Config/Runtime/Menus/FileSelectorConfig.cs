namespace Config.Runtime.Menus;

/// <summary>
/// The config class for the file selector config.
/// </summary>
public sealed class FileSelectorConfig {
    /// <summary>
    /// Constructor for the file selector config object.
    /// </summary>
    /// <param name="fileSelectorWindowId">The file selector window ID config.</param>
    /// <param name="fileSelectorChoiceBoxId">The file selector choice box ID config.</param>
    /// <param name="fontId">The file selector font ID config.</param>
    /// <param name="fontSize">The file selector font size config.</param>
    /// <param name="maxFilesOneScreen">The maximum number of files on one screen config.</param>
    /// <param name="fileMessageWindowWidth">The file message window width config.</param>
    /// <param name="fileMessageWindowHeight">The file message window height config.</param>
    /// <param name="fileMessageWindowX">The file message window X config.</param>
    /// <param name="fileMessageWindowY">The file message window Y config.</param>
    /// <param name="fileSaveMessage">The save message config.</param>
    /// <param name="fileLoadMessage">The load message config.</param>
    /// <param name="warningMessageWindowWidth">The warning message window width config.</param>
    /// <param name="warningMessageWindowHeight">The warning message window height config.</param>
    /// <param name="warningMessageWindowX">The warning message window X config.</param>
    /// <param name="warningMessageWindowY">The warning message window Y config.</param>
    /// <param name="warningMessage">The warning message config.</param>
    /// <param name="warningChoiceWindowWidth">The warning choice window width config.</param>
    /// <param name="warningChoiceWindowHeight">The warning choice window height config.</param>
    /// <param name="warningChoiceWindowX">The warning choice window X config.</param>
    /// <param name="warningChoiceWindowY">The warning choice window Y config.</param>
    public FileSelectorConfig(int fileSelectorWindowId, int fileSelectorChoiceBoxId, int fontId, int fontSize, 
        int maxFilesOneScreen, int fileMessageWindowWidth, int fileMessageWindowHeight, int fileMessageWindowX, 
        int fileMessageWindowY, string saveMessage, string loadMessage, int warningMessageWindowWidth, 
        int warningMessageWindowHeight, int warningMessageWindowX, int warningMessageWindowY, string warningMessage, 
        int warningChoiceWindowWidth, int warningChoiceWindowHeight, int warningChoiceWindowX, 
        int warningChoiceWindowY) {
        this.fileSelectorWindowId = fileSelectorWindowId;
        this.fileSelectorChoiceBoxId = fileSelectorChoiceBoxId;
        this.fontId = fontId;
        this.fontSize = fontSize;
        this.maxFilesOneScreen = maxFilesOneScreen;
        this.fileMessageWindowWidth = fileMessageWindowWidth;
        this.fileMessageWindowHeight = fileMessageWindowHeight;
        this.fileMessageWindowX = fileMessageWindowX;
        this.fileMessageWindowY = fileMessageWindowY;
        this.saveMessage = saveMessage;
        this.loadMessage = loadMessage;
        this.warningMessageWindowWidth = warningMessageWindowWidth;
        this.warningMessageWindowHeight = warningMessageWindowHeight;
        this.warningMessageWindowX = warningMessageWindowX;
        this.warningMessageWindowY = warningMessageWindowY;
        this.warningMessage = warningMessage;
        this.warningChoiceWindowWidth = warningChoiceWindowWidth;
        this.warningChoiceWindowHeight = warningChoiceWindowHeight;
        this.warningChoiceWindowX = warningChoiceWindowX;
        this.warningChoiceWindowY = warningChoiceWindowY;
    }

    /// <summary>
    /// Getter for the file selector window ID config.
    /// </summary>
    /// <returns>The file selector window ID config.</returns>
    public int GetFileSelectorWindowId() {
        return fileSelectorWindowId;
    }

    /// <summary>
    /// Getter for the file choice box ID config.
    /// </summary>
    /// <returns>The file choice box ID config.</returns>
    public int GetFileSelectorChoiceBoxId() {
        return fileSelectorChoiceBoxId;
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
    /// Getter for the max files one screen config.
    /// </summary>
    /// <returns>The max files one screen config.</returns>
    public int GetMaxFilesOneScreen() {
        return maxFilesOneScreen;
    }

    /// <summary>
    /// Getter for the file message window width config.
    /// </summary>
    /// <returns>The file message window width config.</returns>
    public int GetFileMessageWindowWidth() {
        return fileMessageWindowWidth;
    }

    /// <summary>
    /// Getter for the file message window height config.
    /// </summary>
    /// <returns>The file message window height config.</returns>
    public int GetFileMessageWindowHeight() {
        return fileMessageWindowHeight;
    }

    /// <summary>
    /// Getter for the file message window X config.
    /// </summary>
    /// <returns>The file message window X config.</returns>
    public int GetFileMessageWindowX() {
        return fileMessageWindowX;
    }

    /// <summary>
    /// Getter for the file message window Y config.
    /// </summary>
    /// <returns>The file message window Y config.</returns>
    public int GetFileMessageWindowY() {
        return fileMessageWindowY;
    }

    /// <summary>
    /// Getter for the save message config.
    /// </summary>
    /// <returns>The save message config.</returns>
    public string GetSaveMessage() {
        return saveMessage;
    }

    /// <summary>
    /// Getter for the load message config.
    /// </summary>
    /// <returns>The load message config.</returns>
    public string GetLoadMessage() {
        return loadMessage;
    }

    /// <summary>
    /// Getter for the warning message window width config.
    /// </summary>
    /// <returns>The warning message window width config.</returns>
    public int GetWarningMessageWindowWidth() {
        return warningMessageWindowWidth;
    }

    /// <summary>
    /// Getter for the warning message window height config.
    /// </summary>
    /// <returns>The warning message window height config.</returns>
    public int GetWarningMessageWindowHeight() {
        return warningMessageWindowHeight;
    }

    /// <summary>
    /// Getter for the warning message window X config.
    /// </summary>
    /// <returns>The warning message window X config.</returns>
    public int GetWarningMessageWindowX() {
        return warningMessageWindowX;
    }

    /// <summary>
    /// Getter for the warning message window Y config.
    /// </summary>
    /// <returns>The warning message window Y config.</returns>
    public int GetWarningMessageWindowY() {
        return warningMessageWindowY;
    }

    /// <summary>
    /// Getter for the warning message config.
    /// </summary>
    /// <returns>The warning message config.</returns>
    public string GetWarningMessage() {
        return warningMessage;
    }

    /// <summary>
    /// Getter for the warning choice window width config.
    /// </summary>
    /// <returns>The warning choice window width config.</returns>
    public int GetWarningChoiceWindowWidth() {
        return warningChoiceWindowWidth;
    }

    /// <summary>
    /// Getter for the warning choice window height config.
    /// </summary>
    /// <returns>The warning choice window height config.</returns>
    public int GetWarningChoiceWindowHeight() {
        return warningChoiceWindowHeight;
    }

    /// <summary>
    /// Getter for the warning choice window X config.
    /// </summary>
    /// <returns>The warning choice window X config.</returns>
    public int GetWarningChoiceWindowX() {
        return warningChoiceWindowX;
    }

    /// <summary>
    /// Getter for the warning choice window Y config.
    /// </summary>
    /// <returns>The warning choice window Y config.</returns>
    public int GetWarningChoiceWindowY() {
        return warningChoiceWindowY;
    }

    private readonly int fileSelectorWindowId, fileSelectorChoiceBoxId, fontId, fontSize, maxFilesOneScreen, 
        fileMessageWindowWidth, fileMessageWindowHeight, fileMessageWindowX, fileMessageWindowY, 
        warningMessageWindowWidth, warningMessageWindowHeight, warningMessageWindowX, warningMessageWindowY, 
        warningChoiceWindowWidth, warningChoiceWindowHeight, warningChoiceWindowX, warningChoiceWindowY;
    private readonly string saveMessage, loadMessage, warningMessage;
}
