using Data.Runtime.Formations.Factories;

using Engine.Input.Mapper;
using Engine.Systems.Animation.Map.Factories;
using Engine.Systems.Combat.Core;
using Engine.Systems.Movement.Factories;
using Engine.Systems.Navigation.Factories;
using Engine.Systems.Rendering.Factories;
using Engine.UI.Render;

using Game.Maps.Services;
using Game.Scripts.Library;

using Infrastructure.Persistance.Services;
using Infrastructure.Rendering.Core;

namespace Engine.Context.Containers;

public sealed class GameServices 
{
    public required MapService MapService { get; init; }
    public required SaveService SaveService { get; init; }

    public required ScriptLibrary ScriptLibrary { get; init; }
    
    public required InputMapper InputMapper { get; init; }

    public required RenderSystem RenderSystem { get; init; }
    public required UIRenderSystem UIRenderSystem { get; init; }

    // Systems.
    public required DamageCalculator DamageCalculator { get; init; }

    // Map factories.
    public required ActorRendererFactory ActorRendererFactory { get; init; }
    public required MapAnimationManagerFactory MapAnimationManagerFactory { get; init; }
    public required MapRendererFactory MapRendererFactory { get; init; }
    public required MovementResolverFactory MovementResolverFactory { get; init; }
    public required NavigationGridFactory NavigationGridFactory { get; init; }
    public required PartyMapRendererFactory PartyMapRendererFactory { get; init; }
    public required WalkAnimationManagerFactory WalkAnimationManagerFactory { get; init; }

    // Battle factories.
    public required BattleRendererFactory BattleRendererFactory { get; init; }
    public required EnemyRendererFactory EnemyRendererFactory { get; init; }
    public required FormationFactory FormationFactory { get; init; }
    public required PartyBattleRendererFactory PartyBattleRendererFactory { get; init; }
}