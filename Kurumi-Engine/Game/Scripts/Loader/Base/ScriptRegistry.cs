using System.Text.Json;

using Game.Scripts.Loader.Exceptions;

using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Loader.Base;

/// <summary>
/// Loads and stores string file name locations for scripts.
/// </summary>
public sealed class ScriptRegistry 
{
    private readonly IReadOnlyDictionary<string, string> _scriptFileNames;
    
    public required string SubPath { get; init; }

    public ScriptRegistry(string registryPath) 
    {
        try 
        {
            var json = File.ReadAllText(registryPath);
            _scriptFileNames = JsonSerializer.Deserialize<IReadOnlyDictionary<string, string>>(json) ?? 
                throw new RegistryFormatException($"The provided registry at: {registryPath} is in an incorrect " +
                    "format.");
        } 
        catch (Exception exception) when (exception is not RegistryFormatException) 
        {
            throw new JsonFileException($"Registry path: {registryPath} was not found, or an error occured opening " + 
                "the file.");
        }
    }

    public string GetScriptFileName(string scriptName) 
    {
        return _scriptFileNames.TryGetValue(scriptName, out var scriptFileName) ? scriptFileName :
            throw new MissingScriptFileNameException($"Script: {scriptName} was not found");
    }
}