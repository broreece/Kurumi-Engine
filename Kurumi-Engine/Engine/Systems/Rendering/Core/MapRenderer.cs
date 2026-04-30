using Config.Runtime.Map;
using Data.Definitions.Maps.Core;
using Data.Models.Maps;
using Engine.Systems.Animation.Map.Base;
using Engine.Systems.Rendering.Base;
using Infrastructure.Database.Base;
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

using SFML.Graphics;
using SFML.System;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// System that renders the map state.
/// </summary>
public sealed class MapRenderer 
{
    private readonly RenderSystem _renderSystem;

    private readonly Registry<Tile> _tileRegistry;

    private readonly TileSheetConfig _tileSheetConfig;

    private readonly IReadOnlyList<TileModel> _tiles;

    private readonly Texture _tileSheetTexture, _animatedTileSheetTexture;

    internal MapRenderer(
        RenderSystem renderSystem, 
        Registry<Tile> tileRegistry, 
        TileSheetConfig tileSheetConfig, 
        IReadOnlyList<TileModel> tiles, 
        Texture tileSheetTexture, 
        Texture animatedTileSheetTexture) 
    {
        _renderSystem = renderSystem;
        _tileRegistry = tileRegistry;
        _tileSheetConfig = tileSheetConfig;
        _tiles = tiles;
        _tileSheetTexture = tileSheetTexture;
        _animatedTileSheetTexture = animatedTileSheetTexture;
    }

    public void Update(ITileFrameAccessor tileFrameAccessor, View view) 
    {
        // Cache the tile config variables.
        var tilesWide = _tileSheetConfig.TileSheetMaxTilesWide;
        var tileWidth = _tileSheetConfig.Width;
        var tileHeight = _tileSheetConfig.Height;

        // Loop for each static tile in the map.
        foreach (var tileModel in _tiles) 
        {
            var tileObjects = tileModel.Objects.Select(_tileRegistry.Get).ToList();
            foreach (var currentTile in tileObjects) 
            {
                if (!currentTile.Animated) 
                {
                    var xLocation = tileModel.X;
                    var yLocation = tileModel.Y;

                    // Load the tile art.
                    var artId = currentTile.ArtId;
                    var tileSheetX = artId % tilesWide;
                    var tileSheetY = artId / tilesWide;

                    var textureRect = new IntRect(
                        tileSheetX * tileWidth, 
                        tileSheetY * tileHeight, 
                        tileWidth, 
                        tileHeight
                    );
                    var sprite = new Sprite(_tileSheetTexture) {
                        TextureRect = textureRect,
                        Position = new Vector2f(xLocation * tileWidth, yLocation * tileHeight)
                    };

                    // Send to render list.
                    _renderSystem.Submit(
                        new RenderCommand() 
                        {
                            Layer = RenderLayer.TileLayer, 
                            Drawable = sprite, 
                            States = RenderStates.Default,
                            View = view
                        }
                    );
                }
            }
        }

        // Loop for each animated tile in the map.
        foreach (var tileModel in _tiles) 
        {
            var tileObjects = tileModel.Objects.Select(_tileRegistry.Get).ToList();
            foreach (var currentTile in tileObjects) 
            {
                if (currentTile.Animated) 
                {
                    var xLocation = tileModel.X;
                    var yLocation = tileModel.Y;

                    // Load the tile art.
                    var artId = currentTile.ArtId;
                    // TODO: (ATSC-01): Based on config we might be able to change this based on number of animated 
                    // tiles wide the animated tile sheet is.
                    var tileSheetX = tileFrameAccessor.GetTileAnimationFrame();
                    var tileSheetY = artId;

                    var textureRect = new IntRect(
                        tileSheetX * tileWidth, 
                        tileSheetY * tileHeight, 
                        tileWidth, 
                        tileHeight
                    );
                    var sprite = new Sprite(_animatedTileSheetTexture) 
                    {
                        TextureRect = textureRect,
                        Position = new Vector2f(xLocation * tileWidth, yLocation * tileHeight)
                    };

                    // Send to render list.
                    _renderSystem.Submit(
                        new RenderCommand() 
                        {
                            Layer = RenderLayer.AnimatedTileLayer, 
                            Drawable = sprite, 
                            States = RenderStates.Default,
                            View = view
                        }
                    );
                }
            }
        }
    }
}