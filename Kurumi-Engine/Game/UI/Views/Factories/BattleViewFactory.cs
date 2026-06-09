// Config.
using Config.Runtime.Battle;

// Data.
using Data.Definitions.Entities.Abilities.Core;

using Data.Runtime.Entities.Core;

// Engine.
using Engine.Assets.Core;

// Game.
using Game.UI.Views.Core;

// Infrastructure.
using Infrastructure.Database.Base;

namespace Game.UI.Views.Factories;

public sealed class BattleViewFactory
{
    // Asset registry.
    private readonly AssetRegistry _assetRegistry;

    // Registries for abilities and skills to access names.
    private readonly Registry<AbilityDefinition> _abilityRegistry;
    private readonly Registry<NamedData> _abilitySetRegistry;

    // Config.
    private readonly BattleWindowConfig _battleWindowConfig;
    private readonly PartyChoicesConfig _partyChoicesConfig;

    public BattleViewFactory(
        AssetRegistry assetRegistry, 
        Registry<AbilityDefinition> abilityRegistry, 
        Registry<NamedData> abilitySetRegistry, 
        BattleWindowConfig battleWindowConfig, 
        PartyChoicesConfig partyChoicesConfig 
    )
    {
        _assetRegistry = assetRegistry;
        _abilityRegistry = abilityRegistry;
        _abilitySetRegistry = abilitySetRegistry;
        _battleWindowConfig = battleWindowConfig;
        _partyChoicesConfig = partyChoicesConfig;
    }

    public BattleView Create(Character[] partyMembers)
    {
        return new BattleView(
            _assetRegistry, 
            _abilityRegistry, 
            _abilitySetRegistry, 
            _battleWindowConfig, 
            _partyChoicesConfig, 
            partyMembers
        );
    }
}