// Config.
using Config.Runtime.Battle;

// Engine.
using Engine.Assets.Base;
using Engine.Assets.Core;

using Engine.Systems.Rendering.Core;

// Infrastructure.
using Infrastructure.Rendering.Core;

// External libraries.
using SFML.Graphics;
using SFML.System;

namespace Engine.Systems.Rendering.Factories;

public sealed class BattleRendererFactory
{
    private readonly AssetRegistry _assetRegistry;

    private readonly RenderSystem _renderSystem;

    private readonly BattleBackgroundSpriteConfig _battleBackgroundSpriteConfig;

    private readonly Vector2u _displaySize;

    public BattleRendererFactory(
        AssetRegistry assetRegistry, 
        RenderSystem renderSystem, 
        BattleBackgroundSpriteConfig battleBackgroundSpriteConfig, 
        Vector2u displaySize
    )
    {
        _assetRegistry = assetRegistry;
        _renderSystem = renderSystem;
        _battleBackgroundSpriteConfig = battleBackgroundSpriteConfig;
        _displaySize = displaySize;
    }

    public BattleRenderer Create(string battleBackgroundArt) 
    {
        var backgroundTexture = _assetRegistry.GetTexture(AssetType.BattleBackgroundArt, battleBackgroundArt);
        var sprite = new Sprite(backgroundTexture);

        // Scale sprite to size of window.
        var textureWidth = _battleBackgroundSpriteConfig.Width;
        var textureHeight = _battleBackgroundSpriteConfig.Height;

        var windowWidth = _displaySize.X;
        var windowHeight = _displaySize.Y;

        float scaleX = (float) windowWidth / textureWidth;
        float scaleY = (float) windowHeight / textureHeight;

        sprite.Scale = new Vector2f(scaleX, scaleY);

        return new BattleRenderer(_renderSystem, sprite);
    }
}