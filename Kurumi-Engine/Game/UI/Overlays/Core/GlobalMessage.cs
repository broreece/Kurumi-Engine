// Config.
using Config.Runtime.Defaults;

// Engine.
using Engine.Assets.Base;
using Engine.Assets.Core;

using Engine.Input.Base;

using Engine.Systems.Rendering.Base;

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

public sealed class GlobalMessage : IUIOverlay
{
    // Elements.
    private readonly UIElement _uiElement;

    // Time based variables.
    private readonly float _timeLimit;
    private float _messageTimer;

    public bool TakesControl => false;

    public UIElement UIElement => _uiElement;

    internal GlobalMessage(
        AssetRegistry assetRegistry, 
        GlobalMessageDefaults globalMessageDefaults, 
        int timeLimit, 
        string message
    )
    {
        _timeLimit = timeLimit;

        var spriteComponentFactory = new SpriteComponentFactory(assetRegistry);
        var textComponentFactory = new TextComponentFactory(assetRegistry);

        var textStyle = new TextStyle()
        {
            FontSize = (uint) globalMessageDefaults.FontSize, 
            FontArt = globalMessageDefaults.FontName 
        };
        var textComponent = textComponentFactory.Create(new TextData() { Text = message}, textStyle);

        var windowComponent = spriteComponentFactory.Create(
            AssetType.Windows,
            new SpriteStyle() { SpriteArt = globalMessageDefaults.WindowName }
        );

        // Create Element.
        var textUIElement = new UIElement()
        {
            UIComponent = textComponent,
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
                Position = new Vector2f(globalMessageDefaults.X, globalMessageDefaults.Y), 
                Size = new Vector2f(globalMessageDefaults.Width, globalMessageDefaults.Height) 
            }, 
            
            LocalOffset = new Vector2f(globalMessageDefaults.TextXOffset, globalMessageDefaults.TextYOffset), 
            Children =
            [
                textUIElement,
            ], 

            RenderLayer = RenderLayer.UIWindow 
        };
    }

    public void Update(float deltaTime) => _messageTimer += deltaTime;

    /// <summary>
    /// Unused interfaced function.
    /// </summary>
    /// <param name="inputState">The current input state of the game.</param>
    public void HandleInput(InputState inputState) {}

    public bool IsFinished() => _messageTimer >= _timeLimit;
}