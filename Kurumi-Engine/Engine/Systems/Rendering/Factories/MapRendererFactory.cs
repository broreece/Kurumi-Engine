using Config.Runtime.Map;
using Data.Definitions.Maps.Core;
using Data.Models.Maps;
using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.Systems.Rendering.Core;
using Infrastructure.Database.Base;
using Infrastructure.Rendering.Core;

namespace Engine.Systems.Rendering.Factories;

public sealed class MapRendererFactory 
{
    private readonly AssetRegistry _assetRegistry;
    private readonly Registry<Tile> _tileRegistry;

    private readonly RenderSystem _renderSystem;

    private readonly TileSheetConfig _tileSheetConfig;

    public MapRendererFactory(
        AssetRegistry assetRegistry, 
        Registry<Tile> tileRegistry, 
        RenderSystem renderSystem, 
        TileSheetConfig tileSheetConfig
    ) 
    {
        _assetRegistry = assetRegistry;
        _tileRegistry = tileRegistry;
        _renderSystem = renderSystem;
        _tileSheetConfig = tileSheetConfig;
    }

    public MapRenderer Create(IReadOnlyList<TileModel> tiles, string tileSheetTextureName) 
    {
        var tileSheetTexture = _assetRegistry.GetTexture(
            AssetType.TileSpriteSheets, 
            tileSheetTextureName
        );
        var animatedTileSheetTexture = _assetRegistry.GetTexture(
            AssetType.AnimatedTileSpriteSheets, 
            tileSheetTextureName
        );
        return new MapRenderer(
            _renderSystem, 
            _tileRegistry, 
            _tileSheetConfig, 
            tiles, 
            tileSheetTexture, 
            animatedTileSheetTexture
        );
    }
}