using Config.Runtime.Map;
using Data.Runtime.Party.Core;
using Engine.Systems.Rendering.Base;
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

using SFML.Graphics;
using SFML.System;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// System that renders the party map state.
/// </summary>
public sealed class PartyMapRenderer 
{
    private readonly RenderSystem _renderSystem;

    private readonly CharacterFieldSpriteConfig _characterFieldSpriteConfig;

    private readonly int _tileWidth;
    private readonly int _tileHeight;

    private readonly Party _party;
    private readonly Texture _partyTexture;

    internal PartyMapRenderer(
        RenderSystem renderSystem, 
        CharacterFieldSpriteConfig characterFieldSpriteConfig, 
        Party party, 
        Texture partyTexture, 
        int tileWidth, 
        int tileHeight) 
    {
        _renderSystem = renderSystem;
        _characterFieldSpriteConfig = characterFieldSpriteConfig;
        _party = party;
        _partyTexture = partyTexture;
        _tileWidth = tileWidth;
        _tileHeight = tileHeight;
    }

    public void Update(View view) 
    {
        // Cache common variables.
        var characterWidth = _characterFieldSpriteConfig.Width;
        var characterHeight = _characterFieldSpriteConfig.Height;

        // Calculate interpolated position.
        float interpolatedX = _party.LastX + (_party.XLocation - _party.LastX) * _party.MovementProgress;
        float interpolatedY = _party.LastY + (_party.YLocation - _party.LastY) * _party.MovementProgress;

        var textureRect = new IntRect(
            _party.WalkAnimationFrame * characterWidth, 
            _party.PartyModel.Facing * characterHeight,
            characterWidth, characterHeight
        );
        var sprite = new Sprite(_partyTexture) 
        {
            TextureRect = textureRect,
            Position = new Vector2f(
                interpolatedX * _tileWidth + (_tileWidth / 2) - (characterWidth / 2), 
                interpolatedY * _tileHeight + _tileHeight - characterHeight
            )
        };

        _renderSystem.Submit(
            new RenderCommand() 
            {
                Layer = RenderLayer.PartyMapLayer, 
                Drawable = sprite, 
                States = RenderStates.Default,
                View = view
            }
        );
    }
}