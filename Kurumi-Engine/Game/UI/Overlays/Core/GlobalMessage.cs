using Config.Runtime.Defaults;

using Engine.Assets.Base;
using Engine.Assets.Core;
using Engine.Input.Base;
using Engine.Systems.Rendering.Base;
using Engine.UI.Components.Factories;
using Engine.UI.Data.Content;
using Engine.UI.Data.Content.Layout;
using Engine.UI.Data.Style;
using Engine.UI.Elements;

using Game.UI.Overlays.Base;

using SFML.System;

namespace Game.UI.Overlays.Core;

public sealed class GlobalMessage : IUIOverlay
{
    // Elements.
    private readonly UIElement _uiElement;

    // Default variables.
    private bool _isFinished = false;

    // TODO: (UI-01) - Implement timer here.

    public GlobalMessage(
        AssetRegistry assetRegistry, 
        GlobalMessageDefaults globalMessageDefaults, 
        int timeLimit, 
        string message)
    {
        // Component factories.
        var spriteComponentFactory = new SpriteComponentFactory(assetRegistry);
        var textComponentFactory = new TextComponentFactory(assetRegistry);

        // Text style.
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

        var width = globalMessageDefaults.Width;
        var height = globalMessageDefaults.Height;
        var xLocation = globalMessageDefaults.X;
        var yLocation = globalMessageDefaults.Y;

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

    public void Update(float deltaTime)
    {
        // TODO: (UICA-01) - Implement timer and fading out here.
    }

    /// <summary>
    /// Unused interfaced function.
    /// </summary>
    /// <param name="inputState">The current input state of the game.</param>
    public void HandleInput(InputState inputState) {}

    public UIElement GetUIElement() => _uiElement;

    public bool IsFinished() => _isFinished;

    public bool TakesControl() => false;
}