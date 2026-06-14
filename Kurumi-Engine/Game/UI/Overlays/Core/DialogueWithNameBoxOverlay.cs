// Config.
using Config.Runtime.Defaults;

// Engine.
using Engine.Assets.Base;
using Engine.Assets.Core;

using Engine.Input.Base;

using Engine.Systems.Rendering.Base;

using Engine.UI.Components.Core;
using Engine.UI.Components.Factories;
using Engine.UI.Data.Content;
using Engine.UI.Data.Content.Layout;
using Engine.UI.Data.Style;
using Engine.UI.Elements;

// Game.
using Game.UI.Overlays.Base;

// External libraries.
using SFML.System;

namespace Game.UI.Overlays.Core;

public sealed class DialogueWithNameBoxOverlay : IUIOverlay
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

    public UIElement UIElement => _uiElement;

    public bool TakesControl => true;

    internal DialogueWithNameBoxOverlay(
        AssetRegistry assetRegistry, 
        NameBoxDefaults nameBoxDefaults, 
        TextWindowDefaults textWindowDefaults, 
        IReadOnlyList<string> pages, 
        string name
    ) 
    {
        // Component factories.
        var spriteComponentFactory = new SpriteComponentFactory(assetRegistry);
        var textComponentFactory = new TextComponentFactory(assetRegistry);

        // Text styles.
        var windowTextStyle = new TextStyle()
        {
            FontSize = (uint) textWindowDefaults.FontSize, 
            FontArt = textWindowDefaults.FontName 
        };
        _textComponent = textComponentFactory.Create(new TextData() { Text = pages[0] }, windowTextStyle);

        var nameTextStyle = new TextStyle()
        {
            FontSize = (uint) nameBoxDefaults.FontSize, 
            FontArt = nameBoxDefaults.FontName 
        };
        var nameTextComponent = textComponentFactory.Create(new TextData() { Text = name }, nameTextStyle);

        _pages = pages;

        var windowComponent = spriteComponentFactory.Create(
            AssetType.Windows,
            new SpriteStyle() { SpriteArt = textWindowDefaults.WindowName }
        );
        var nameWindowComponent = spriteComponentFactory.Create(
            AssetType.Windows,
            new SpriteStyle() { SpriteArt = nameBoxDefaults.WindowName }
        );

        var width = textWindowDefaults.Width;
        var height = textWindowDefaults.Height;
        var xLocation = textWindowDefaults.X;
        var yLocation = textWindowDefaults.Y;
        var textXOffset = textWindowDefaults.TextXOffset;
        var textYOffset = textWindowDefaults.TextYOffset;

        var nameWidth = nameBoxDefaults.Width;
        var nameHeight = nameBoxDefaults.Height;
        var nameXLocation = nameBoxDefaults.X;
        var nameYLocation = nameBoxDefaults.Y;
        var nameTextXOffset = nameBoxDefaults.TextXOffset;
        var nameTextYOffset = nameBoxDefaults.TextYOffset;

        // Create Elements.
        var textUIElement = new UIElement()
        {
            UIComponent = _textComponent,
            Layout = new UILayout { Position = new Vector2f(0, 0), Size = new Vector2f(1, 1) },
            
            LocalOffset = new Vector2f(textXOffset, textYOffset),
            Children = [],

            RenderLayer = RenderLayer.UIText
        };
        var nameTextUIElement = new UIElement()
        {
            UIComponent = nameTextComponent,
            Layout = new UILayout { Position = new Vector2f(0, 0), Size = new Vector2f(1, 1) },
            
            LocalOffset = new Vector2f(nameTextXOffset, nameTextYOffset),
            Children = [],

            RenderLayer = RenderLayer.UIText
        };

        var textWindowElement = new UIElement()
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
                textUIElement
            ],

            RenderLayer = RenderLayer.UIWindow
        };

        var nameWindowElement = new UIElement()
        {
            UIComponent = nameWindowComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(nameXLocation, nameYLocation), 
                Size = new Vector2f(nameWidth, nameHeight) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children =
            [
                nameTextUIElement
            ],

            RenderLayer = RenderLayer.UIWindow
        };

        // The parent UI element of the name window and text window.
        _uiElement = new UIElement()
        {
            UIComponent = new EmptyComponent(),
            Layout = new UILayout() 
            { 
                Position = new Vector2f(0, 0), 
                Size = new Vector2f(1, 1) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children = 
            [
                nameWindowElement,
                textWindowElement
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
        if (_currentPage < _pages.Count - 1) 
        {
            _currentPage ++;
            _textComponent.SetText(_pages[_currentPage]);
        }
        else 
        {
            _isFinished = true;
        }
    }

    public bool IsFinished() => _isFinished;
}