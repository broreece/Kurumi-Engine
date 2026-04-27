using Engine.Systems.Rendering.Base;
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

using SFML.Graphics;
using SFML.System;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// Used to render the party state on the current map.
/// </summary>
public sealed class ActorRenderer 
{
    private readonly RenderSystem renderSystem;
    private readonly IReadOnlyList<ActorRenderData> actorRenderData;
    
    private readonly int tileWidth;
    private readonly int tileHeight;

    internal ActorRenderer(
        RenderSystem renderSystem, 
        IReadOnlyList<ActorRenderData> actorRenderData, 
        int tileWidth, 
        int tileHeight) 
    {

        this.renderSystem = renderSystem;
        this.actorRenderData = actorRenderData;
        this.tileWidth = tileWidth;
        this.tileHeight = tileHeight;
    }

    public void Update() 
    {
        foreach (var currentActorRenderData in actorRenderData) 
        {
            // Cache common variables.
            var actorWidth = currentActorRenderData.Width;
            var actorHeight = currentActorRenderData.Height;
            var actor = currentActorRenderData.Actor;

            // Calculate the render layer.
            var layer = actor.ActorInfo.BelowParty ? RenderLayer.BelowPartyActor : RenderLayer.AbovePartyActor;

            // Calculate interpolated position.
            float interpolatedX = actor.LastX + (actor.XLocation - actor.LastX) * actor.MovementProgress;
            float interpolatedY = actor.LastY + (actor.YLocation - actor.LastY) * actor.MovementProgress;

            var textureRect = new IntRect(
                actor.WalkAnimationFrame * actorWidth, 
                actor.ActorModel.Facing * actorHeight, 
                actorWidth, 
                actorHeight
            );
            var sprite = new Sprite(currentActorRenderData.Texture) 
            {
                TextureRect = textureRect,
                Position = new Vector2f(
                    interpolatedX * tileWidth + (tileWidth / 2) - (actorWidth / 2),
                    interpolatedY * tileHeight + tileHeight - actorHeight
                )
            };

            renderSystem.Submit(new RenderCommand() { Layer = layer, Drawable = sprite, States = RenderStates.Default });
        }
    }
}