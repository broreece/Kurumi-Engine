// Config.
using Config.Runtime.Map;

// Data.
using Data.Definitions.Maps.Core;

using Data.Models.Maps;

// Engine.
using Engine.Assets.Base;
using Engine.Assets.Core;

using Engine.Systems.Rendering.Core;

// Infrastructure.
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

    public MapRenderer Create(
        IReadOnlyList<TileModel> tiles, 
        string tileSheetName, 
        string animatedTileSheetName, 
        string mapBackgroundName
    ) 
    {
        var tileSheetTexture = _assetRegistry.GetTexture(
            AssetType.TileSpriteSheets, 
            tileSheetName
        );

        var animatedTileSheetTexture = _assetRegistry.GetTexture(
            AssetType.AnimatedTileSpriteSheets, 
            animatedTileSheetName
        );

        var mapBackgroundTexture = _assetRegistry.GetTexture(
            AssetType.MapBackgroundArt, 
            mapBackgroundName
        );
        
        return new MapRenderer(
            _renderSystem, 
            _tileRegistry, 
            _tileSheetConfig, 
            tiles, 
            tileSheetTexture, 
            animatedTileSheetTexture,
            mapBackgroundTexture
        );
    }
}