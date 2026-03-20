namespace Config.Runtime.Menus;

/// <summary>
/// The config class for the inventory config.
/// </summary>
public sealed class InventoryConfig {
    /// <summary>
    /// Constructor for the inventory window config.
    /// </summary>
    /// <param name="inventoryWindowId">The inventory window ID config.</param>
    /// <param name="inventoryChoiceBoxId">The inventory choice box ID config.</param>
    /// <param name="fontId">The inventory font ID config.</param>
    /// <param name="fontSize">The inventory font size config.</param>
    /// <param name="inventoryWindowWidth">The inventory window width config.</param>
    /// <param name="inventoryWindowHeight">The inventory window height config.</param>
    /// <param name="inventoryWindowX">The inventory window X config.</param>
    /// <param name="inventoryWindowY">The inventory window Y config.</param>
    /// <param name="descriptionWindowWidth">The description window width config.</param>
    /// <param name="descriptionWindowHeight">The description window height config.</param>
    /// <param name="descriptionWindowX">The description window X config.</param>
    /// <param name="descriptionWindowY">The description window Y config.</param>
    public InventoryConfig(int inventoryWindowId, int inventoryChoiceBoxId, int fontId, int fontSize, 
        int inventoryWindowWidth, int inventoryWindowHeight, int inventoryWindowX, int inventoryWindowY, 
        int descriptionWindowWidth, int descriptionWindowHeight, int descriptionWindowX, int descriptionWindowY) {
        this.inventoryWindowId = inventoryWindowId;
        this.inventoryChoiceBoxId = inventoryChoiceBoxId;
        this.fontId = fontId;
        this.fontSize = fontSize;
        this.inventoryWindowWidth = inventoryWindowWidth;
        this.inventoryWindowHeight = inventoryWindowHeight;
        this.inventoryWindowX = inventoryWindowX;
        this.inventoryWindowY = inventoryWindowY;
        this.descriptionWindowWidth = descriptionWindowWidth;
        this.descriptionWindowHeight = descriptionWindowHeight;
        this.descriptionWindowX = descriptionWindowX;
        this.descriptionWindowY = descriptionWindowY;
    }

    /// <summary>
    /// Getter for the inventory window ID config.
    /// </summary>
    /// <returns>The inventory window ID config.</returns>
    public int GetWindowId() {
        return inventoryWindowId;
    }

    /// <summary>
    /// Getter for the inventory choice box ID config.
    /// </summary>
    /// <returns>The inventory choice box ID config.</returns>
    public int GetChoiceBoxId() {
        return inventoryChoiceBoxId;
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
    /// Getter for the inventory window width config.
    /// </summary>
    /// <returns>The inventory window width config.</returns>
    public int GetInventoryWindowWidth() {
        return inventoryWindowWidth;
    }

    /// <summary>
    /// Getter for the inventory window height config.
    /// </summary>
    /// <returns>The inventory window height config.</returns>
    public int GetInventoryWindowHeight() {
        return inventoryWindowHeight;
    }

    /// <summary>
    /// Getter for the inventory window X config.
    /// </summary>
    /// <returns>The inventory window X config.</returns>
    public int GetInventoryWindowX() {
        return inventoryWindowX;
    }

    /// <summary>
    /// Getter for the inventory window Y config.
    /// </summary>
    /// <returns>The inventory window Y config.</returns>
    public int GetInventoryWindowY() {
        return inventoryWindowY;
    }

    /// <summary>
    /// Getter for the description window width config.
    /// </summary>
    /// <returns>The description window width config.</returns>
    public int GetDescriptionWindowWidth() {
        return descriptionWindowWidth;
    }

    /// <summary>
    /// Getter for the description window height config.
    /// </summary>
    /// <returns>The description window height config.</returns>
    public int GetDescriptionWindowHeight() {
        return descriptionWindowHeight;
    }

    /// <summary>
    /// Getter for the description window X config.
    /// </summary>
    /// <returns>The description window X config.</returns>
    public int GetDescriptionWindowX() {
        return descriptionWindowX;
    }

    /// <summary>
    /// Getter for the description window Y config.
    /// </summary>
    /// <returns>The description window Y config.</returns>
    public int GetDescriptionWindowY() {
        return descriptionWindowY;
    }

    private readonly int inventoryWindowId, inventoryChoiceBoxId, fontId, fontSize, inventoryWindowWidth, 
        inventoryWindowHeight, inventoryWindowX, inventoryWindowY, descriptionWindowWidth, descriptionWindowHeight, 
        descriptionWindowX, descriptionWindowY;
}