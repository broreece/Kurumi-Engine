namespace Config.Runtime.Battle;

/// <summary>
/// The config class for the party choices battle config.
/// </summary>
public sealed class PartyChoicesConfig {
    /// <summary>
    /// The constructor for the party choices config.
    /// </summary>
    /// <param name="itemsEnabled">Boolean for determining if items are enabled.</param>
    /// <param name="runAwayEnabled">Boolean for determining if run away is enabled.</param>
    /// <param name="itemsText">The items choice text.</param>
    /// <param name="runAwayText">The run away choice text.</param>
    public PartyChoicesConfig(bool itemsEnabled, bool runAwayEnabled, string itemsText, string runAwayText) {
        this.itemsEnabled = itemsEnabled;
        this.runAwayEnabled = runAwayEnabled;
        this.itemsText = itemsText;
        this.runAwayText = runAwayText;
    }
    /// <summary>
    /// Getter for if the items option is enabled.
    /// </summary>
    /// <returns>If the items are enabled in battle.</returns>
    public bool ItemsEnabled() {
        return itemsEnabled;
    }

    /// <summary>
    /// Getter for if run away option is enabled.
    /// </summary>
    /// <returns>If the run away option is enabled in battle.</returns>
    public bool RunAwayEnabled() {
        return runAwayEnabled;
    }

    /// <summary>
    /// Getter for the items choice text.
    /// </summary>
    /// <returns>The items choice text.</returns>
    public string GetItemsText() {
        return itemsText;
    }

    /// <summary>
    /// Getter for the run away choice text.
    /// </summary>
    /// <returns>The run away choice text.</returns>
    public string GetRunAwayText() {
        return runAwayText;
    }

    private readonly bool itemsEnabled, runAwayEnabled;
    private readonly string itemsText, runAwayText;
}