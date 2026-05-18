using Data.Definitions.Actors.Core;
using Data.Runtime.Actors.Core;
using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.Systems.Rendering.Base;
using Engine.Systems.Rendering.Core;
using Infrastructure.Database.Base;
using Infrastructure.Rendering.Core;

namespace Engine.Systems.Rendering.Factories;

public sealed class ActorRendererFactory 
{
    private readonly AssetRegistry _assetRegistry;
    private readonly Registry<ActorSprite> _actorSpriteRegistry;

    private readonly RenderSystem _renderSystem;

    private readonly int _tileWidth;
    private readonly int _tileHeight;

    public ActorRendererFactory(
        AssetRegistry assetRegistry, 
        Registry<ActorSprite> actorSpriteRegistry, 
        RenderSystem renderSystem, 
        int tileWidth, 
        int tileHeight) 
    {   
        _assetRegistry = assetRegistry;
        _renderSystem = renderSystem;
        _actorSpriteRegistry = actorSpriteRegistry;
        _tileWidth = tileWidth;
        _tileHeight = tileHeight;
    }

    public ActorRenderer Create(IReadOnlyList<Actor> actors) 
    {
        // Load actor render data.
        var actorRenderData = new List<ActorRenderData>();
        foreach (var actor in actors) {
            var actorSprite = _actorSpriteRegistry.Get(actor.SpriteId);

            // Load actor texture.
            var actorTexture = _assetRegistry.GetTexture(AssetType.ActorSpriteSheets, actorSprite.SpriteName);
            actorRenderData.Add(new ActorRenderData() 
            {
                Width = actorSprite.Width, 
                Height = actorSprite.Height, 
                Texture = actorTexture, 
                Actor = actor
            });
        }
        return new ActorRenderer(_renderSystem, actorRenderData, _tileWidth, _tileHeight);
    }
}