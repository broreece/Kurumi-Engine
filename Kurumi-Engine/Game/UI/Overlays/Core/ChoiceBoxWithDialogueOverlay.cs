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

public sealed class ChoiceBoxWithDialogueOverlay : IUIOverlay 
{
    // Elements.
    private readonly UIElement _uiElement;
    private UIElement _selectionElement;

    // Number of choices.
    private readonly int _numberOfChoices;

    // Default variables.
    private bool _isFinished = false;
    private int _currentChoice = 0;
    private int _choiceChange = 0;

    // Stored config.
    private int _choiceSpacing;

    public bool YesSelected => _currentChoice == 0;

    public ChoiceBoxWithDialogueOverlay(
        AssetRegistry assetRegistry, 
        TextWindowDefaults textWindowDefaults, 
        ChoiceBoxDefaults choiceBoxDefaults,
        IReadOnlyList<string> choices,
        string text)
    {
        var textObject = new Text(text, assetRegistry.GetFont(textWindowDefaults.FontName));
        var textComponent = new TextComponent(textObject);

        var choiceTextComponents = new List<TextComponent>();
        foreach (var choice in choices)
        {
            var choiceTextObject = new Text(choice, assetRegistry.GetFont(choiceBoxDefaults.FontName));
            choiceTextComponents.Add(new TextComponent(choiceTextObject));
        }

        _numberOfChoices = choices.Count;

        var windowComponent = new SpriteComponent(assetRegistry.GetTexture(
            AssetType.Windows, 
            textWindowDefaults.WindowName
        ));
        var choiceWindowComponent = new SpriteComponent(assetRegistry.GetTexture(
            AssetType.Windows, 
            choiceBoxDefaults.WindowName
        ));
        var selectionComponent = new SpriteComponent(assetRegistry.GetTexture(
            AssetType.ChoiceSelectionArt,
            choiceBoxDefaults.ChoiceBoxName
        ));

        var textWindowWidth = textWindowDefaults.Width;
        var textWindowHeight = textWindowDefaults.Height;
        var textWindowXLocation = textWindowDefaults.X;
        var textWindowYLocation = textWindowDefaults.Y;
        
        var choiceWindowWidth = choiceBoxDefaults.Width;
        var choiceWindowHeight = choiceBoxDefaults.Height;
        var selectionHeight = choiceBoxDefaults.Height / _numberOfChoices;
        var choiceWindowXLocation = choiceBoxDefaults.X;
        var choiceWindowYLocation = choiceBoxDefaults.Y;
        _choiceSpacing = choiceBoxDefaults.Spacing;

        // Create Elements.
        // Text window.
        var textUIElement = new UIElement()
        {
            UIComponent = textComponent,
            Layout = new UILayout { Position = new Vector2f(0, 0), Size = new Vector2f(1, 1) },
            
            LocalOffset = new Vector2f(0, 0),
            Children = [],

            RenderLayer = RenderLayer.UIText
        };
        var textWindowElement = new UIElement()
        {
            UIComponent = windowComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(textWindowXLocation, textWindowYLocation), 
                Size = new Vector2f(textWindowWidth, textWindowHeight) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children =
            [
                textUIElement,
            ],

            RenderLayer = RenderLayer.UIWindow
        };

        // Choice box window.
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
            
            LocalOffset = new Vector2f(0, _choiceSpacing * choiceIndex),
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

            RenderLayer = RenderLayer.UIWindow
        };
        var choiceWindowElement = new UIElement()
        {
            UIComponent = choiceWindowComponent,
            Layout = new UILayout() 
            { 
                Position = new Vector2f(choiceWindowXLocation, choiceWindowYLocation), 
                Size = new Vector2f(choiceWindowWidth, choiceWindowHeight) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children = [.. choiceTextElements, _selectionElement],

            RenderLayer = RenderLayer.UIWindow
        };

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
        var width = _selectionElement.Layout.Size.X;
        var height = _selectionElement.Layout.Size.Y;
        var xLocation = _selectionElement.Layout.Position.X;
        var yLocation = _selectionElement.Layout.Position.Y;
        _selectionElement.Layout = new UILayout() {
            Position = new Vector2f(xLocation, yLocation + (_choiceChange * _choiceSpacing)),
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

    public UIElement GetUIElement() => _uiElement;

    public bool IsFinished() => _isFinished;

    public bool TakesControl() => true;
}
