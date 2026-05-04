using Config.Runtime.Defaults;

using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.Input.Base;
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
    // Components.
    private readonly TextComponent _textComponent;

    // Elements.
    private readonly UIElement _uiElement;

    // Pages.
    private readonly IReadOnlyList<string> _pages;

    // Default variables.
    private bool _isFinished = false;
    private int _currentPage = 0;

    public DialogueOverlay(
        AssetRegistry assetRegistry, 
        TextWindowDefaults textWindowDefaults, 
        IReadOnlyList<string> pages) 
    {
        var textObject = new Text(pages[0], assetRegistry.GetFont(textWindowDefaults.FontName));
        _textComponent = new TextComponent(textObject);

        _pages = pages;

        var windowComponent = new WindowComponent(assetRegistry.GetTexture(
            AssetType.Windows, 
            textWindowDefaults.WindowName
        ));

        var width = textWindowDefaults.Width;
        var height = textWindowDefaults.Height;
        var xLocation = textWindowDefaults.X;
        var yLocation = textWindowDefaults.Y;

        // Create Element.
        var textUIElement = new UIElement()
        {
            UIComponent = _textComponent,
            Layout = new UILayout { Position = new Vector2f(0, 0), Size = new Vector2f(1, 1) },
            
            LocalOffset = new Vector2f(0, 0),
            Children = [],

            RenderLayer = RenderLayer.UIText
        };

        _uiElement = new UIElement()
        {
            UIComponent = windowComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(xLocation, yLocation), 
                Size = new Vector2f(width, height) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children =
            [
                textUIElement,
            ],

            RenderLayer = RenderLayer.UIWindow
        };
    }

    public void Update(float deltaTime) {}

    public void HandleInput(InputState inputState)
    {
        bool confirmPressed = inputState.IsPressed(InputAction.Confirm);
        if (confirmPressed)
        {
            Advance();
        }
    }

    public void Advance() 
    {
        if (_currentPage < _pages.Count - 1) {
            _currentPage ++;
            _textComponent.SetText(_pages[_currentPage]);
        }
        else {
            _isFinished = true;
        }
    }

    public UIElement GetUIElement() => _uiElement;

    public bool IsFinished() => _isFinished;

    public bool TakesControl() => true;
}