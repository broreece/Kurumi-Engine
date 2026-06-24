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
    private TextComponent? _textComponent;

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
        _pages = pages;

        var spriteComponentFactory = new SpriteComponentFactory(assetRegistry);
        var textComponentFactory = new TextComponentFactory(assetRegistry);

        var textWindowElement = CreateTextElement(
            spriteComponentFactory, 
            textComponentFactory, 
            textWindowDefaults, 
            pages
        );
        var nameWindowElement = CreateNameElement(
            spriteComponentFactory, 
            textComponentFactory, 
            nameBoxDefaults, 
            name
        );

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
            _textComponent!.SetText(_pages[_currentPage]);
        }
        else 
        {
            _isFinished = true;
        }
    }

    public bool IsFinished() => _isFinished;

    private UIElement CreateNameElement(
        SpriteComponentFactory spriteComponentFactory, 
        TextComponentFactory textComponentFactory, 
        NameBoxDefaults nameBoxDefaults, 
        string name
    )
    {
        var nameTextStyle = new TextStyle()
        {
            FontSize = (uint) nameBoxDefaults.FontSize, 
            FontArt = nameBoxDefaults.FontName 
        };
        var nameTextComponent = textComponentFactory.Create(new TextData() { Text = name }, nameTextStyle);

        var nameWindowComponent = spriteComponentFactory.Create(
            AssetType.Windows,
            new SpriteStyle() { SpriteArt = nameBoxDefaults.WindowName }
        );

        var nameTextUIElement = new UIElement()
        {
            UIComponent = nameTextComponent,
            Layout = new UILayout { Position = new Vector2f(0, 0), Size = new Vector2f(1, 1) },
            
            LocalOffset = new Vector2f(nameBoxDefaults.TextXOffset, nameBoxDefaults.TextYOffset),
            Children = [],

            RenderLayer = RenderLayer.UIText
        };

        var nameWindowElement = new UIElement()
        {
            UIComponent = nameWindowComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(nameBoxDefaults.X, nameBoxDefaults.Y), 
                Size = new Vector2f(nameBoxDefaults.Width, nameBoxDefaults.Height) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children =
            [
                nameTextUIElement
            ],

            RenderLayer = RenderLayer.UIWindow
        };

        return nameWindowElement;
    }

    private UIElement CreateTextElement(
        SpriteComponentFactory spriteComponentFactory, 
        TextComponentFactory textComponentFactory, 
        TextWindowDefaults textWindowDefaults, 
        IReadOnlyList<string> pages
    )
    {
        var windowTextStyle = new TextStyle()
        {
            FontSize = (uint) textWindowDefaults.FontSize, 
            FontArt = textWindowDefaults.FontName 
        };
        _textComponent = textComponentFactory.Create(new TextData() { Text = pages[0] }, windowTextStyle);

        var windowComponent = spriteComponentFactory.Create(
            AssetType.Windows,
            new SpriteStyle() { SpriteArt = textWindowDefaults.WindowName }
        );

        var textUIElement = new UIElement()
        {
            UIComponent = _textComponent,
            Layout = new UILayout { Position = new Vector2f(0, 0), Size = new Vector2f(1, 1) },
            
            LocalOffset = new Vector2f(textWindowDefaults.TextXOffset, textWindowDefaults.TextYOffset),
            Children = [],

            RenderLayer = RenderLayer.UIText
        };

        var textWindowElement = new UIElement()
        {
            UIComponent = windowComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(textWindowDefaults.X, textWindowDefaults.Y), 
                Size = new Vector2f(textWindowDefaults.Width, textWindowDefaults.Height) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children =
            [
                textUIElement
            ],

            RenderLayer = RenderLayer.UIWindow
        };

        return textWindowElement;
    }
}