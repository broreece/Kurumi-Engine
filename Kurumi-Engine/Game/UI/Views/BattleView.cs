using Engine.Assets.Core;
using Engine.UI.Components.Factories;
using Engine.UI.Data.Content.Layout;
using Engine.UI.Data.Style;
using Engine.UI.Elements;

using SFML.System;

namespace Game.UI.Views;

public sealed class BattleView
{
    private readonly WindowStyle _windowStyle;
    private readonly TextStyle _textStyle;

    private readonly AssetRegistry _assetRegistry;

    public BattleView(WindowStyle windowStyle, TextStyle textStyle, AssetRegistry assetRegistry)
    {
        _windowStyle = windowStyle;
        _textStyle = textStyle;
        _assetRegistry = assetRegistry;
    }

    public UIElement Build()
    {
        var windowComponentFactory = new WindowComponentFactory(_assetRegistry);
        var windowComponent = windowComponentFactory.Create(_windowStyle);

        return new UIElement
        {
            UIComponent = windowComponent,
            Layout = new UILayout() { Position = new Vector2f(0,0), Size = new Vector2f(50, 50) },
            
            LocalOffset = new Vector2f(0, 0),
            Children =
            [
                // CreateOption("Attack", 0),
                // CreateOption("Skill", 1),
                // CreateOption("Item", 2),
                // CreateOption("Run", 3)
            ]
        };
    }

    // private UIElement CreateOption(string text, int index)
    // {
    //     return new UIElement
    //     {
    //         UIComponent = ,
    //         Layout = ,

    //         LocalOffset = new Vector2f(0, 0),
    //         Children = []
    //     };
    // }
}