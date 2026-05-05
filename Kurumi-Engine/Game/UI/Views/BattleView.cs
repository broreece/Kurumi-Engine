using Config.Runtime.Battle;

using Data.Runtime.Entities.Core;

using Engine.Assets.Base;
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
    // Components.
    private readonly List<TextComponent> _partyTextComponents = [];
    private List<TextComponent> _choiceTextComponents = [];

    // Elements.
    public UIElement UIElement { get; }

    public BattleView(AssetRegistry assetRegistry, BattleWindowConfig battleWindowConfig, int partySize)
    {
        // Style config.
        var battleWindowName = battleWindowConfig.WindowArtName;
        var fontSize = (uint) battleWindowConfig.FontSize;
        var fontArt = battleWindowConfig.FontName;

        var windowStyle = new SpriteStyle() { SpriteArt = battleWindowName };
        var textStyle = new TextStyle() { FontSize = fontSize, FontArt = fontArt};

        // Component factories.
        var spriteComponentFactory = new SpriteComponentFactory(assetRegistry);
        var textComponentFactory = new TextComponentFactory(assetRegistry);

        // Info window variables.
        // Config.
        var infoXLocation = battleWindowConfig.InfoWindowX;
        var infoYLocation = battleWindowConfig.InfoWindowY;
        var infoWidth = battleWindowConfig.InfoWindowWidth;
        var infoHeight = battleWindowConfig.InfoWindowHeight;
        var spacing = battleWindowConfig.SelectionWindowSpacing;

        var infoWindowComponent = spriteComponentFactory.Create(AssetType.Windows, windowStyle);
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
            var component = textComponentFactory.Create(infoTextData, textStyle);
            _partyTextComponents.Add(component);

            // Place UI element children.
            childrenElements.Add(new UIElement()
            {
                UIComponent = component,
                Layout = new UILayout() 
                {
                    Position = new Vector2f(0, partyIndex * spacing),
                    Size = new Vector2f(1, 1)
                },

                LocalOffset = new Vector2f(0, 0),
                Children =  [],

                RenderLayer = RenderLayer.UIText
            });
        }

        var infoWindowElement = new UIElement()
        {
            UIComponent = infoWindowComponent,
            Layout = infoLayout,
            
            LocalOffset = new Vector2f(0, 0),
            Children = childrenElements,

            RenderLayer = RenderLayer.UIWindow
        };

        // Selection window variables.
        // Config.
        var selectionWindowXLocation = battleWindowConfig.SelectionWindowX;
        var selectionWindowYLocation = battleWindowConfig.SelectionWindowY;
        var selectionWindowWidth = battleWindowConfig.SelectionWindowWidth;
        var selectionWindowHeight = battleWindowConfig.SelectionWindowHeight;

        var selectionWindowComponent = spriteComponentFactory.Create(AssetType.Windows, windowStyle);
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

        UIElement = new UIElement()
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
    /// <param name="currentCharacterIndex">The index of the current character.</param>
    public void Update(Character[] partyMembers, int currentCharacterIndex)
    {
        // Party information.
        for (int partyIndex = 0; partyIndex < partyMembers.Length; partyIndex ++)
        {
            if (partyMembers[partyIndex] != null)
            {
                var character = partyMembers[partyIndex];
                _partyTextComponents[partyIndex].SetText($"{character.Name} HP: {character.CurrentHP} / " +
                    $"{character.MaxHP}, MP: {character.CurrentMP}: {character.MaxMP}");
            }
        }

        // Choices.
        _choiceTextComponents = [];
        var currentCharacter = partyMembers[currentCharacterIndex];
        // Base abilities.
        foreach (var id in currentCharacter.GetAbilityIDs())
        {
            
        }
        // Ability sets.
        foreach (var keyPair in currentCharacter.GetAbilitySetIDs())
        {
            
        }
        // Additional battle options.
    }
}