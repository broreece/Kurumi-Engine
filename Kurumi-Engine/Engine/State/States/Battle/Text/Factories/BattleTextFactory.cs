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

    public BattleText Create(string text, int xLocation, int yLocation, BattleTextType textType)
    {
        return new BattleText() 
        { 
            FontName = _battleTextConfig.BattleFontName, 
            Text = text, 
            XLocation = xLocation, 
            YLocation = yLocation, 
            TextType = textType, 
            Timer = _battleTextConfig.BattleDisplayLength 
        };
    }
}