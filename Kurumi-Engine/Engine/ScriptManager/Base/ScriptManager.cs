namespace Engine.ScriptManager.Base;

using Utils.Exceptions;
using System.Text.Json;

/// <summary>
/// The script manager class, loads and stores string file name locations for scripts.
/// </summary>
public abstract class ScriptManager {
    public ScriptManager(string registryPath) {
        // Load json file.
        string json;
        Dictionary<string, string> data;
        try {
            json = File.ReadAllText(registryPath);
            data = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? throw new Exception();;
        } 
        catch (Exception) {
            throw new MissingJsonFileException($"Registry path: {registryPath} not found or invalid format");
        }

        // Store file names in array.
        scriptFileNames = [.. data.Values];
    }

    /// <summary>
    /// Gets a specified script file name.
    /// </summary>
    /// <param name="index">The index of the desired script file name.</param>
    /// <returns>The specific script file name.</returns>
    public string GetScriptFileName(int index) {
        return scriptFileNames[index];
    }

    protected readonly string[] scriptFileNames;
}