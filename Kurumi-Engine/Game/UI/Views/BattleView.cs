using Config.Runtime.Battle;

using Engine.Assets.Core;
using Engine.UI.Components.Core;
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

    private readonly BattleWindowConfig _battleWindowConfig;

    private readonly AssetRegistry _assetRegistry;

    public BattleView(BattleWindowConfig battleWindowConfig, AssetRegistry assetRegistry)
    {
        // Style config.
        var battleWindowName = battleWindowConfig.WindowArtName;
        var fontSize = (uint) battleWindowConfig.FontSize;
        var fontArt = battleWindowConfig.FontName;

        _windowStyle = new WindowStyle() { WindowArt = battleWindowName };
        _textStyle = new TextStyle() { FontSize = fontSize, FontArt = fontArt};
        _battleWindowConfig = battleWindowConfig;
        _assetRegistry = assetRegistry;
    }

    public UIElement Build()
    {
        var windowComponentFactory = new WindowComponentFactory(_assetRegistry);

        // Info window variables.
        // Config.
        var infoXLocation = _battleWindowConfig.InfoWindowX;
        var infoYLocation = _battleWindowConfig.InfoWindowY;
        var infoWidth = _battleWindowConfig.InfoWindowWidth;
        var infoHeight = _battleWindowConfig.InfoWindowHeight;

        var infoWindowComponent = windowComponentFactory.Create(_windowStyle);
        var infoLayout = new UILayout() 
        { 
            Position = new Vector2f(infoXLocation, infoYLocation), 
            Size = new Vector2f(infoWidth, infoHeight) 
        };
        var infoWindowElement = new UIElement()
        {
            UIComponent = infoWindowComponent,
            Layout = infoLayout,
            
            LocalOffset = new Vector2f(0, 0),
            Children = []
        };

        // Selection window variables.
        // Config.
        var selectionWindowXLocation = _battleWindowConfig.SelectionWindowX;
        var selectionWindowYLocation = _battleWindowConfig.SelectionWindowY;
        var selectionWindowWidth = _battleWindowConfig.SelectionWindowWidth;
        var selectionWindowHeight = _battleWindowConfig.SelectionWindowHeight;

        var selectionWindowComponent = windowComponentFactory.Create(_windowStyle);
        var selectionWindowLayout = new UILayout() 
        { 
            Position = new Vector2f(selectionWindowXLocation, selectionWindowYLocation), 
            Size = new Vector2f(selectionWindowWidth, selectionWindowHeight) 
        };
        var selectionWindowElement = new UIElement() 
        { 
            UIComponent = selectionWindowComponent, 
            Layout = selectionWindowLayout,

            LocalOffset = new Vector2f(0, 0),
            Children = []
        };

        return new UIElement()
        {
            UIComponent = new EmptyComponent(),
            Layout = new UILayout { Position = new Vector2f(0, 0), Size = new Vector2f(100, 100) },
            
            LocalOffset = new Vector2f(0, 0),
            Children =
            [
                infoWindowElement,
                selectionWindowElement
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