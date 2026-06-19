// Engine.
using Engine.Systems.Rendering.Base;

// Infrastructure.
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

// External libraries.
using SFML.Graphics;
using SFML.System;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// Used to render the party state on the current map.
/// </summary>
public sealed class ActorRenderer 
{
    private readonly RenderSystem _renderSystem;
    private readonly IReadOnlyList<ActorRenderData> _actorRenderData;
    
    private readonly int _tileWidth;
    private readonly int _tileHeight;

    internal ActorRenderer(
        RenderSystem renderSystem, 
        IReadOnlyList<ActorRenderData> actorRenderData, 
        int tileWidth, 
        int tileHeight
    ) 
    {
        _renderSystem = renderSystem;
        _actorRenderData = actorRenderData;
        _tileWidth = tileWidth;
        _tileHeight = tileHeight;
    }

    public void Update(View view) 
    {
        foreach (var currentActorRenderData in _actorRenderData) 
        {
            // Cache common variables.
            var actorWidth = currentActorRenderData.Width;
            var actorHeight = currentActorRenderData.Height;
            var actor = currentActorRenderData.Actor;

            // Calculate the render layer.
            var layer = actor.BelowParty ? RenderLayer.BelowPartyActor : RenderLayer.AbovePartyActor;

            // Calculate interpolated position.
            float interpolatedX = actor.LastX + (actor.XLocation - actor.LastX) * actor.MovementProgress;
            float interpolatedY = actor.LastY + (actor.YLocation - actor.LastY) * actor.MovementProgress;

            var textureRect = new IntRect(
                actor.WalkAnimationFrame * actorWidth, 
                actor.SpriteState * actorHeight, 
                actorWidth, 
                actorHeight
            );
            var sprite = new Sprite(currentActorRenderData.Texture) 
            {
                TextureRect = textureRect,
                Position = new Vector2f(
                    interpolatedX * _tileWidth + (_tileWidth / 2) - (actorWidth / 2),
                    interpolatedY * _tileHeight + _tileHeight - actorHeight
                )
            };

            _renderSystem.Submit(new RenderCommand() { 
                Layer = layer, 
                SubmissionIndex = (int) interpolatedY, 
                Drawable = sprite, 
                States = RenderStates.Default, 
                View = view
            });
        }
    }
}