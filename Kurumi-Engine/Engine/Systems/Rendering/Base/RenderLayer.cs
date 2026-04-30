namespace Engine.Systems.Rendering.Base;

/// <summary>
/// The game's render layer enum. Used to store different layers as readable integer values.
/// </summary>
public enum RenderLayer 
{
    BackgroundLayer = 0,

    TileLayer = 1,
    AnimatedTileLayer = 2,
    BelowPartyActor = 3,
    PartyMapLayer = 4,
    AbovePartyActor = 5,

    BelowPartyBattleLayer = 6,
    PartyBattleLayer = 7,
    AbovePartyBattleLayer = 8,
    BaseEnemyLayer = 9,
    EnemyBodyPartsLayer = 10,

    UIWindow = 11,
    UIText = 12
}