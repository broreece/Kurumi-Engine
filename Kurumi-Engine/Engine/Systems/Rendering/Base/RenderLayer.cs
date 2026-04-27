namespace Engine.Systems.Rendering.Base;

/// <summary>
/// The game's render layer enum. Used to store different layers as readable integer values.
/// </summary>
public enum RenderLayer 
{
    BackgroundLayer = 0,

    TileLayer = 1,
    BelowPartyActor = 2,
    PartyMapLayer = 3,
    AbovePartyActor = 4,

    BelowPartyBattleLayer = 7,
    PartyBattleLayer = 6,
    AbovePartyBattleLayer = 8,
    BaseEnemyLayer = 9,
    EnemyBodyPartsLayer = 10,

    UIWindow = 11,
    UIText = 12
}