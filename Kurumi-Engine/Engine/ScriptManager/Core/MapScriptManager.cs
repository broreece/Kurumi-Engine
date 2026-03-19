namespace Engine.ScriptManager.Core;

using Engine.ScriptManager.Base;
using Engine.ScriptManager.Serialization;
using Scripts.MapScripts.Base;
using Utils.Exceptions;
using System.Text.Json;

/// <summary>
/// The script manager class, loads and stores string file name locations for scripts.
/// </summary>
public sealed class MapScriptManager : ScriptManager {
    public MapScriptManager(string registryPath) : base(registryPath) {}

    /// <summary>
    /// Deseralizes a specified script id and then returns that script object.
    /// </summary>
    /// <param name="index">The index of the specific script..</param>
    /// <exception cref="MissingJsonFileException">Error thrown if a .json data file is missing.</exception>
    public MapScript LoadMapScript(int index) {
        // Load from passed file.
        ScriptData scriptData;
        try {
            string fullRegistryPath = Path.Combine(
                AppContext.BaseDirectory,
                scriptFileNames[index]
            );
            var json = File.ReadAllText(fullRegistryPath);
            scriptData = JsonSerializer.Deserialize<ScriptData>(json) ?? 
                throw new Exception();
        }
        catch (Exception) {
            throw new MissingJsonFileException($"Script file: {scriptFileNames[index]} could not be found or contains an invalid format");
        }

        // Load base values.
        string scriptName = scriptData.Name;

        // Loop over each step.
        int size = scriptData.Steps.Count;
        for (int stepIndex = 0; stepIndex < size; stepIndex ++) {
            
        }
    }
}