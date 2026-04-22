namespace Engine.Systems.Rendering.Base;

/// <summary>
/// The game's render layer enum. Used to store different layers as readable integer values.
/// </summary>
public enum RenderLayer 
{
    BackgroundLayer = 0,
    TileLayer = 1,
    BelowPartyActor = 2,
    Party = 3,
    AbovePartyActor = 4
}