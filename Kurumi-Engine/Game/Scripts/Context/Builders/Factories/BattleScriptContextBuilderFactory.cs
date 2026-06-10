// Data.
using Data.Runtime.Formations.Core;

// Game.
using Game.Scripts.Context.Builder.Core;
using Game.Scripts.Context.Capabilities.Implementations.Battle.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Entity.Factories;
using Game.Scripts.Context.Capabilities.Implementations.Universal.Core;

namespace Game.Scripts.Context.Builder.Factories;

public sealed class BattleScriptContextBuilderFactory
{
    // Factories.
    private readonly ActiveBattleActionsFactory _activeBattleActionsFactory;

    private readonly HpMpActionsFactory _hpMpActionsFactory;

    private readonly UIActionsFactory _uiActionsFactory;

    public BattleScriptContextBuilderFactory(
        ActiveBattleActionsFactory activeBattleActionsFactory,
        HpMpActionsFactory hpMpActionsFactory, 
        UIActionsFactory uiActionsFactory
    )
    {
        _activeBattleActionsFactory = activeBattleActionsFactory;
        _hpMpActionsFactory = hpMpActionsFactory;
        _uiActionsFactory = uiActionsFactory;
    }

    public BattleScriptContextBuilder Create(Formation formation)
    {
        return new BattleScriptContextBuilder(
            _activeBattleActionsFactory, 
            _hpMpActionsFactory, 
            _uiActionsFactory, 
            formation
        );
    }
}