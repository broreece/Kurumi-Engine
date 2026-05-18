using Game.Scripts.Core;
using Game.Scripts.Factories;
using Game.Scripts.Loader.Base;
using Game.Scripts.Loader.Core;
using Game.Scripts.Services;

namespace Game.Scripts.Library;

/// <summary>
/// Contains the registries for the script data and the service to convert the raw data into script runtime objects.
/// </summary>
public sealed class ScriptLibrary 
{
    private readonly ScriptFactory _scriptFactory;
    private readonly ScriptLoader _mapScriptLoader, _battleScriptLoader, _entityScriptLoader;

    private static readonly string _mapPath = Path.Combine("Assets", "Scripts", "Map");
    private static readonly string _battlePath = Path.Combine("Assets", "Scripts", "Battle");
    private static readonly string _entityPath = Path.Combine("Assets", "Scripts", "Entity");

    public ScriptLibrary(string basePath) 
    {
        // Load registries and then loaders.
        var mapScriptRegistry = new ScriptRegistry(Path.Combine(
            basePath,
            "map_script_registry.json"
        )) { SubPath = _mapPath };
        _mapScriptLoader = new ScriptLoader(mapScriptRegistry);

        var battleScriptRegistry = new ScriptRegistry(Path.Combine(
            basePath,
            "battle_script_registry.json"
        )) { SubPath = _battlePath };
        _battleScriptLoader = new ScriptLoader(battleScriptRegistry);

        var entityScriptRegistry = new ScriptRegistry(Path.Combine(
            basePath,
            "entity_script_registry.json"
        )) { SubPath = _entityPath };
        _entityScriptLoader = new ScriptLoader(entityScriptRegistry);

        // Create factory for scripts.
        var scriptDataConverter = new ScriptDataConverter();
        _scriptFactory = new ScriptFactory(scriptDataConverter);
    }

    public Script GetMapScript(string key) => _scriptFactory.Create(_mapScriptLoader.LoadScriptData(key));

    public Script GetBattleScript(string key) => _scriptFactory.Create(_battleScriptLoader.LoadScriptData(key));

    public Script GetEntityScript(string key) => _scriptFactory.Create(_entityScriptLoader.LoadScriptData(key));
}