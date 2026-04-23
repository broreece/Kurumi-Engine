using SFML.Graphics;

namespace Engine.Systems.Rendering.Base;

public sealed class PartyMemberBattleRenderData 
{
    public required int Index { get; init; }

    public required Texture Texture { get; init; }
}