using System.Text.Json;

using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Loader.Base;

/// <summary>
/// Loads and stores string file name locations for scripts.
/// </summary>
public sealed class ScriptRegistry 
{
    private readonly IReadOnlyDictionary<string, string> _scriptFileNames;

    public ScriptRegistry(string registryPath) 
    {
        // TODO: (MLE-01) Create specific exceptions here.
        try 
        {
            var json = File.ReadAllText(registryPath);
            _scriptFileNames = JsonSerializer.Deserialize<IReadOnlyDictionary<string, string>>(json) ?? 
                throw new Exception();
        } 
        catch (Exception) 
        {
            throw new JsonFileException($"Registry path: {registryPath} not found or invalid format");
        }
    }

    public string GetScriptFileName(string scriptName) 
    {
        // TODO: (DKE-01) Add exception if key isn't found.
        return _scriptFileNames[scriptName];
    }
}