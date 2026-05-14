using System.Text.Json;

using Game.Scripts.Loader.Base;
using Game.Scripts.Serialization;
using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Loader.Core;

/// <summary>
/// Loads the JSON script data from a specified file.
/// </summary>
public sealed class ScriptLoader {
    private readonly ScriptRegistry _registry;

    public ScriptLoader(ScriptRegistry registry) 
    {
        _registry = registry;
    }

    public ScriptData LoadScriptData(string scriptName) 
    {
        // TODO: (MLE-01) Create specific exceptions here.
        try 
        {
            var scriptPath = Path.Combine(
                AppContext.BaseDirectory,
                _registry.SubPath,
                _registry.GetScriptFileName(scriptName)
            );
            var json = File.ReadAllText(scriptPath);
            return JsonSerializer.Deserialize<ScriptData>(json) ?? throw new Exception();
        } 
        catch (Exception) 
        {
            throw new JsonFileException($"Script: {scriptName} not found or invalid format");
        }
    }
}