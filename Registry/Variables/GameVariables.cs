namespace Registry.Variables;

using Save.Core;
using Save.Serialization.VariablesData;

/// <summary>
/// The game variables sealed class, contains a set of flags and variables that can be adjusted throughout gameplay.
/// </summary>
public sealed class GameVariables {
    /// <summary>
    /// The constructor for the game variables class.
    /// </summary>
    /// <param name="saveManager">The save manager object that directly interacts with .json save data.</param>
    public GameVariables(SaveManager saveManager) {
        VariablesData gameVariableData = saveManager.LoadVariables();
        gameVariables = gameVariableData.Variables.Values.ToArray();
        variableNames = gameVariableData.Variables.Keys.ToArray();
        gameFlags = gameVariableData.Flags.Values.Select(v => v != 0).ToArray();
        flagNames = gameVariableData.Flags.Keys.ToArray();
    }

    /// <summary>
    /// Getter for the array of game variables.
    /// </summary>
    /// <returns>Array of game variables.</returns>
    public int[] GetGameVariables() {
        return gameVariables;
    }

    /// <summary>
    /// Getter for a specific game variable in the game variables array.
    /// </summary>
    /// <param name="index">The index of the desired game variable.</param>
    /// <returns>The specified game variable.</returns>
    public int GetGameVariable(int index) {
        return gameVariables[index];
    }
    
    /// <summary>
    /// Unique setter allowing setting a fixed game variable a value.
    /// </summary>
    /// <param name="index">The index of the game variable.</param>
    /// <param name="value">The new value.</param>
    public void SetGameVariable(int index, int value) {
        gameVariables[index] = value;
    }

    /// <summary>
    /// Getter for the array of game flags.
    /// </summary>
    /// <returns>Array of game flags.</returns>
    public bool[] GetGameFlags() {
        return gameFlags;
    }

    /// <summary>
    /// Getter for a specificed game flag within the game flags array.
    /// </summary>
    /// <param name="flagIndex">The index of the game flag.</param>
    /// <returns>Specified game flag.</returns>
    public bool GetGameFlag(int flagIndex) {
        return gameFlags[flagIndex];
    }
    
    /// <summary>
    /// Unique setter allowing setting a fixed game flag a value.
    /// </summary>
    /// <param name="index">The index of the game flag.</param>
    /// <param name="value">The new value.</param>
    public void SetGameFlag(int index, bool value) {
        gameFlags[index] = value;
    }

    /// <summary>
    /// Getter for a specific variable name in the variable names array.
    /// </summary>
    /// <param name="index">The index of the desired variable name.</param>
    /// <returns>A specified variable name.</returns>
    public string GetGameVariableName(int index) {
        return variableNames[index];
    }

    /// <summary>
    /// Getter for a specific flag name in the flag names array.
    /// </summary>
    /// <param name="index">The index of the desired flag name.</param>
    /// <returns>A specified flag name.</returns>
    public string GetGameFlagName(int index) {
        return flagNames[index];
    }

    private readonly int[] gameVariables;
    private readonly bool[] gameFlags;
    private readonly string[] variableNames, flagNames;
}