using Engine.Systems.Rendering.Base;
using Infrastructure.Rendering.Core;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// System that renders the party battle state.
/// </summary>
public sealed class PartyBattleRenderer 
{
    private readonly RenderSystem _renderSystem;

    private readonly PartyMemberBattleRenderData[] _partyMemberBattleRenderData;

    internal PartyBattleRenderer(RenderSystem renderSystem, PartyMemberBattleRenderData[] partyMemberBattleRenderData)
    {
        _renderSystem = renderSystem;
        _partyMemberBattleRenderData = partyMemberBattleRenderData;
    }
    
    public void Update()
    {
        
    }
}