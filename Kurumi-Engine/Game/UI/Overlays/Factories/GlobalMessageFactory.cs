// Config.
using Config.Runtime.Defaults;

// Engine.
using Engine.Assets.Core;

// Game.
using Game.UI.Overlays.Core;

namespace Game.UI.Overlays.Factories;

public sealed class GlobalMessageFactory
{
    private readonly AssetRegistry _assetRegistry;
    private readonly GlobalMessageDefaults _globalMessageDefaults;

    public GlobalMessageFactory(AssetRegistry assetRegistry, GlobalMessageDefaults globalMessageDefaults)
    {
        _assetRegistry = assetRegistry;
        _globalMessageDefaults = globalMessageDefaults;
    }

    public GlobalMessage Create(int timeLimit, string message)
    {
        return new GlobalMessage(_assetRegistry, _globalMessageDefaults, timeLimit, message);
    }
}