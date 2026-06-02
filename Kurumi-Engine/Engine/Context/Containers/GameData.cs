// Config.
using Config.Core;

// Engine.
using Engine.Assets.Core;

// Game.
using Game.Scripts.Library;

// Infrastructure.
using Infrastructure.Database.Database;

namespace Engine.Context.Containers;

public sealed class GameData 
{
    public required ConfigProvider ConfigProvider { get; init; }
    public required GameDatabase GameDatabase { get; init; }
    public required AssetRegistry AssetRegistry { get; init; }
    public required ScriptLibrary ScriptLibrary { get; init; }
}