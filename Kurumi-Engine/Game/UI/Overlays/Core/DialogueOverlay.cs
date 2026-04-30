using Config.Runtime.Defaults;

using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.Systems.Rendering.Base;
using Engine.UI.Components.Core;
using Engine.UI.Data.Content.Layout;
using Engine.UI.Elements;

using Game.UI.Overlays.Base;

using SFML.Graphics;
using SFML.System;

namespace Game.UI.Overlays.Core;

public sealed class DialogueOverlay : IUIOverlay
{
    private readonly TextComponent _textComponent;
    private readonly WindowComponent _windowComponent;

    private readonly IReadOnlyList<string> _pages;

    private readonly int _width, _height, _xLocation, _yLocation;
    private int _currentPage = 0;

    public DialogueOverlay(
        AssetRegistry assetRegistry, 
        TextWindowDefaults textWindowDefaults, 
        IReadOnlyList<string> pages) 
    {
        _windowComponent = new WindowComponent(assetRegistry.GetTexture(
            AssetType.Windows, 
            textWindowDefaults.WindowName
        ));

        var textObject = new Text(pages[0], assetRegistry.GetFont(textWindowDefaults.FontName));
        _textComponent = new TextComponent(textObject);

        _width = textWindowDefaults.Width;
        _height = textWindowDefaults.Height;
        _xLocation = textWindowDefaults.X;
        _yLocation = textWindowDefaults.Y;

        _pages = pages;
    }

    public void Update(float deltaTime) {}

    public UIElement Build()
    {
        var textUIElement = new UIElement()
        {
            UIComponent = _textComponent,
            Layout = new UILayout { Position = new Vector2f(0, 0), Size = new Vector2f(1, 1) },
            
            LocalOffset = new Vector2f(0, 0),
            Children = [],

            RenderLayer = RenderLayer.UIText
        };

        return new UIElement()
        {
            UIComponent = _windowComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(_xLocation, _yLocation), 
                Size = new Vector2f(_width, _height) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children =
            [
                textUIElement,
            ],

            RenderLayer = RenderLayer.UIWindow
        };
    }

    public void Advance() 
    {
        if (_currentPage < _pages.Count - 1) {
            _currentPage ++;
            _textComponent.SetText(_pages[_currentPage]);
        }
        else {
            // TODO: End here.
        }
    }

}