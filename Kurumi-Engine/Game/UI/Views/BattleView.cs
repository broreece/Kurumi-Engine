using Config.Runtime.Battle;
using Data.Runtime.Entities.Core;
using Engine.Assets.Core;
using Engine.Systems.Rendering.Base;
using Engine.UI.Components.Core;
using Engine.UI.Components.Factories;
using Engine.UI.Data.Content;
using Engine.UI.Data.Content.Layout;
using Engine.UI.Data.Style;
using Engine.UI.Elements;

using SFML.System;

namespace Game.UI.Views;

public sealed class BattleView
{
    // Styles.
    private readonly WindowStyle _windowStyle;
    private readonly TextStyle _textStyle;

    // Config.
    private readonly BattleWindowConfig _battleWindowConfig;

    // Asset registry.
    private readonly AssetRegistry _assetRegistry;

    // Components.
    private readonly List<TextComponent> _partyTextComponents = [];

    public BattleView(AssetRegistry assetRegistry, BattleWindowConfig battleWindowConfig)
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

    public UIElement Build(int partySize)
    {
        // Component factories.
        var windowComponentFactory = new WindowComponentFactory(_assetRegistry);
        var textComponentFactory = new TextComponentFactory(_assetRegistry);

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

        // Info window text.
        var childrenElements = new List<UIElement>();
        for (int partyIndex = 0; partyIndex < partySize; partyIndex ++)
        {
            // Create and store components in array to be updated later.
            var infoTextData = new TextData() { Text = "" };
            var component = textComponentFactory.Create(infoTextData, _textStyle);
            _partyTextComponents.Add(component);

            // Place UI element children.
            childrenElements.Add(new UIElement()
            {
                UIComponent = component,
                Layout = new UILayout() 
                {
                    // TODO: Update position based on index here.
                    Position = new Vector2f(0, 0),
                    Size = new Vector2f(1, 1)
                },

                LocalOffset = new Vector2f(0, 0),
                Children =  [],

                RenderLayer = RenderLayer.UIText
            });
        }

        var infoTextContainerElement = new UIElement()
        {
            UIComponent = new EmptyComponent(),
            Layout = new UILayout() 
            {
                Position = new Vector2f(0, 0),
                Size = new Vector2f(1, 1)
            },

            LocalOffset = new Vector2f(0, 0),
            Children =  childrenElements,

            RenderLayer = RenderLayer.UIText
        };

        var infoWindowElement = new UIElement()
        {
            UIComponent = infoWindowComponent,
            Layout = infoLayout,
            
            LocalOffset = new Vector2f(0, 0),
            Children = 
            [
                infoTextContainerElement
            ],

            RenderLayer = RenderLayer.UIWindow
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
            Children = [],

            RenderLayer = RenderLayer.UIWindow
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
            ],

            RenderLayer = RenderLayer.UIWindow
        };
    }

    /// <summary>
    /// Updates the battle UI view based on the state of the party members.
    /// </summary>
    /// <param name="partyMembers">The array of playable characters in the party.</param>
    public void Update(Character[] partyMembers)
    {
        for (int partyIndex = 0; partyIndex < partyMembers.Length; partyIndex ++)
        {
            if (partyMembers[partyIndex] != null)
            {
                var character = partyMembers[partyIndex];
                _partyTextComponents[partyIndex].SetText($"{character.Name} HP: {character.CurrentHP} / " +
                    $"{character.MaxHP}, MP: {character.CurrentMP}: {character.MaxMP}");
            }
        }
    }
}