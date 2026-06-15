// Config.
using Config.Runtime.Battle;

// Engine.
using Engine.State.States.Battle.Text.Base;
using Engine.State.States.Battle.Text.Core;

namespace Engine.State.States.Battle.Text.Factories;

public sealed class BattleTextFactory 
{
    private readonly BattleTextConfig _battleTextConfig;

    public BattleTextFactory(BattleTextConfig battleTextConfig)
    {
        _battleTextConfig = battleTextConfig;
    }

    public BattleText Create(string text, BattleTextType textType)
    {
        return new BattleText(_battleTextConfig.BattleDisplayLength) 
        { 
            FontName = _battleTextConfig.BattleFontName, 
            Text = text, 
            FontSize = (uint) _battleTextConfig.BattleFontSize, 
            TextType = textType 
        };
    }
}