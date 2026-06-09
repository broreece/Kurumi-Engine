// Engine.
using Engine.State.States.Battle.Text.Core;

using Engine.Systems.Rendering.Base;

// Infrastructure.
using Infrastructure.Rendering.Base;
using Infrastructure.Rendering.Core;

// External libraries.
using SFML.Graphics;

namespace Engine.Systems.Rendering.Core;

/// <summary>
/// System that renders the battle state.
/// </summary>
public sealed class BattleRenderer 
{
    private readonly RenderSystem _renderSystem;

    private readonly Sprite _backgroundSprite;

    private readonly Font _battleTextFont;

    private readonly IList<BattleText> _battleTexts;

    internal BattleRenderer(
        RenderSystem renderSystem, 
        Sprite backgroundSprite, 
        Font battleTextFont, 
        IList<BattleText> battleTexts
    )
    {
        _renderSystem = renderSystem;
        _backgroundSprite = backgroundSprite;
        _battleTextFont = battleTextFont;
        _battleTexts = battleTexts;
    }

    public void Update(View view, float deltaTime)
    {
        foreach (BattleText battleText in _battleTexts)
        {
            battleText.Update(deltaTime);
            if (!battleText.Finished)
            {
                var text = new Text(battleText.Text, _battleTextFont, battleText.FontSize);
                _renderSystem.Submit(
                    new RenderCommand()
                    {
                        Layer = RenderLayer.UIText, 
                        SubmissionIndex = 0, 
                        Drawable = text, 
                        States = RenderStates.Default, 
                        View = view 
                    }
                );
            }
        }

        // Send to render list.
        _renderSystem.Submit(
            new RenderCommand() 
            {
                Layer = RenderLayer.BackgroundLayer, 
                SubmissionIndex = 0, 
                Drawable = _backgroundSprite, 
                States = RenderStates.Default,
                View = view
            }
        );
    }
}