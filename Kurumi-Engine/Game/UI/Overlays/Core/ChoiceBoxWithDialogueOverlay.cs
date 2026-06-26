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

public sealed class ChoiceBoxWithDialogueOverlay : IUIOverlay 
{
    // Elements.
    private readonly UIElement _uiElement;
    private UIElement? _selectionElement;

    // Number of choices.
    private readonly int _numberOfChoices;

    // Default variables.
    private bool _isFinished = false;
    private int _currentChoice = 0;
    private int _choiceChange = 0;

    // Stored config.
    private int _spacing;

    public UIElement UIElement => _uiElement;

    public bool TakesControl => true;

    public bool YesSelected => _currentChoice == 0;

    internal ChoiceBoxWithDialogueOverlay(
        AssetRegistry assetRegistry, 
        TextWindowDefaults textWindowDefaults, 
        ChoiceBoxDefaults choiceBoxDefaults, 
        IReadOnlyList<string> choices, 
        string text
    )
    {
        _numberOfChoices = choices.Count;
        _spacing = choiceBoxDefaults.Spacing;

        var spriteComponentFactory = new SpriteComponentFactory(assetRegistry);
        var textComponentFactory = new TextComponentFactory(assetRegistry);

        var textWindowElement = CreateTextElement(
            spriteComponentFactory, 
            textComponentFactory, 
            textWindowDefaults, 
            text
        );
        var choiceWindowElement = CreateChoiceElement(
            spriteComponentFactory, 
            textComponentFactory, 
            choiceBoxDefaults, 
            choices
        );

        // The parent UI element of the choice window and text window.
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
                choiceWindowElement,
                textWindowElement
            ],

            RenderLayer = RenderLayer.UIWindow
        };
    }

    public void Update(float deltaTime)
    {
        // Update the position of the selection element based on the new current choice.
        var width = _selectionElement!.Layout.Size.X;
        var height = _selectionElement.Layout.Size.Y;
        var xLocation = _selectionElement.Layout.Position.X;
        var yLocation = _selectionElement.Layout.Position.Y;
        _selectionElement.Layout = new UILayout() {
            Position = new Vector2f(xLocation, yLocation + (_choiceChange * _spacing)),
            Size = new Vector2f(width, height)
        };
        _choiceChange = 0;
    }

    public void HandleInput(InputState inputState)
    {
        bool confirmPressed = inputState.IsPressed(InputAction.Confirm);
        bool downPressed = inputState.IsPressed(InputAction.MoveDown);
        bool upPressed = inputState.IsPressed(InputAction.MoveUp);
        if (confirmPressed)
        {
            _isFinished = true;
        }
        // Wrap choice around if it goes out of bounds.
        else if (downPressed)
        {
            var oldChoice = _currentChoice;
            _currentChoice ++;
            if (_currentChoice >= _numberOfChoices)
            {
                _currentChoice = 0;
            }
            _choiceChange = (_currentChoice + 1) - (oldChoice + 1);
        }
        else if (upPressed)
        {
            var oldChoice = _currentChoice;
            _currentChoice --;
            if (_currentChoice < 0)
            {
                _currentChoice = _numberOfChoices - 1;
            }
            _choiceChange = (_currentChoice + 1) - (oldChoice + 1);
        }
    }

    public bool IsFinished() => _isFinished;

    private UIElement CreateTextElement(
        SpriteComponentFactory spriteComponentFactory, 
        TextComponentFactory textComponentFactory, 
        TextWindowDefaults textWindowDefaults, 
        string text
    )
    {
        var windowComponent = spriteComponentFactory.Create(
            AssetType.Windows,
            new SpriteStyle() { SpriteArt = textWindowDefaults.WindowName }
        );

        var windowTextStyle = new TextStyle()
        {
            FontSize = (uint) textWindowDefaults.FontSize, 
            FontArt = textWindowDefaults.FontName 
        };

        var textComponent = textComponentFactory.Create(new TextData() { Text = text }, windowTextStyle);

        var textUIElement = new UIElement()
        {
            UIComponent = textComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(0, 0), 
                Size = new Vector2f(1, 1) 
            },
            
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
                textUIElement,
            ],

            RenderLayer = RenderLayer.UIWindow
        };

        return textWindowElement;
    }

    private UIElement CreateChoiceElement(
        SpriteComponentFactory spriteComponentFactory, 
        TextComponentFactory textComponentFactory, 
        ChoiceBoxDefaults choiceBoxDefaults, 
        IReadOnlyList<string> choices
    )
    {
        var choiceTextStyle = new TextStyle() 
        { 
            FontSize = (uint) choiceBoxDefaults.FontSize, 
            FontArt = choiceBoxDefaults.FontName 
        };

        var choiceWindowComponent = spriteComponentFactory.Create(
            AssetType.Windows, 
            new SpriteStyle() { SpriteArt = choiceBoxDefaults.WindowName }
        );
        var selectionComponent = spriteComponentFactory.Create(
            AssetType.ChoiceSelectionArt,
            new SpriteStyle() { SpriteArt = choiceBoxDefaults.ChoiceBoxName }
        );

        var choiceTextComponents = new List<TextComponent>();
        foreach (var choice in choices)
        {
            choiceTextComponents.Add(textComponentFactory.Create(new TextData() { Text = choice }, choiceTextStyle));
        }

        // Reused element variables.
        var choiceWindowWidth = choiceBoxDefaults.Width;
        var selectionHeight = choiceBoxDefaults.Height / _numberOfChoices;

        var choiceTextElements = new List<UIElement>();
        var choiceIndex = 0;

        foreach (TextComponent choiceTextComponent in choiceTextComponents)
        {
            choiceTextElements.Add(new UIElement()
            {
                UIComponent = choiceTextComponent,
                Layout = new UILayout() 
            { 
                Position = new Vector2f(0, 0), 
                Size = new Vector2f(1, 1) 
            },
            
            LocalOffset = new Vector2f(
                choiceBoxDefaults.TextXOffset, 
                (_spacing * choiceIndex) + choiceBoxDefaults.TextYOffset
            ),
            Children = [],

            RenderLayer = RenderLayer.UISelectionBox
            });
            choiceIndex ++;
        }

        _selectionElement = new UIElement()
        {
            UIComponent = selectionComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(0, 0), 
                Size = new Vector2f(choiceWindowWidth, selectionHeight) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children = [],

            RenderLayer = RenderLayer.UISelectionBox
        };
        
        var choiceWindowElement = new UIElement()
        {
            UIComponent = choiceWindowComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(choiceBoxDefaults.X, choiceBoxDefaults.Y), 
                Size = new Vector2f(choiceWindowWidth, choiceBoxDefaults.Height) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children = [.. choiceTextElements, _selectionElement],

            RenderLayer = RenderLayer.UIWindow
        };

        return choiceWindowElement;
    }
}
