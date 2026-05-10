using Config.Runtime.Battle;

using Data.Definitions.Entities.Abilities.Core;
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

using Infrastructure.Database.Base;

using SFML.System;

namespace Game.UI.Views;

public sealed class BattleView
{
    // Registries for abilities and skills to access names.
    private readonly Registry<AbilityDefinition> _abilityRegistry;
    private readonly Registry<NamedData> _abilitySetRegistry;

    // Party members.
    private readonly Character[] _partyMembers;

    // Cached config.
    private readonly PartyChoicesConfig _partyChoicesConfig;
    private readonly int _maxChoicesPerPage;
    private readonly int _spacing;

    // Component factories.
    private readonly SpriteComponentFactory _spriteComponentFactory;
    private readonly TextComponentFactory _textComponentFactory;

    // Cached styles.
    private readonly TextStyle _textStyle;

    // Components and children elements.
    private readonly UIElement _selectionWindowElement;
    private readonly UIElement _selectionElement;
    private readonly List<TextComponent> _partyTextComponents = [];

    // Choice selection location variables.
    private int _currentChoice = 0;

    // Elements.
    public UIElement UIElement { get; }

    public BattleView(
        AssetRegistry assetRegistry, 
        Registry<AbilityDefinition> abilityRegistry, 
        Registry<NamedData> abilitySetRegistry,
        BattleWindowConfig battleWindowConfig, 
        PartyChoicesConfig partyChoicesConfig,
        Character[] partyMembers
    )
    {
        _abilityRegistry = abilityRegistry;
        _abilitySetRegistry = abilitySetRegistry;
        _partyMembers = partyMembers;

        // Style config.
        var battleWindowName = battleWindowConfig.WindowArtName;
        var selectionName = battleWindowConfig.ChoiceBoxArtName;
        var fontSize = (uint) battleWindowConfig.FontSize;
        var fontArt = battleWindowConfig.FontName;

        var windowStyle = new SpriteStyle() { SpriteArt = battleWindowName };
        var selectionStyle = new SpriteStyle() { SpriteArt = selectionName };
        _textStyle = new TextStyle() { FontSize = fontSize, FontArt = fontArt};

        // Component factories.
        _spriteComponentFactory = new SpriteComponentFactory(assetRegistry);
        _textComponentFactory = new TextComponentFactory(assetRegistry);

        // Info window variables.
        // Config.
        _partyChoicesConfig = partyChoicesConfig;
        var infoXLocation = battleWindowConfig.InfoWindowX;
        var infoYLocation = battleWindowConfig.InfoWindowY;
        var infoWidth = battleWindowConfig.InfoWindowWidth;
        var infoHeight = battleWindowConfig.InfoWindowHeight;

        _spacing = battleWindowConfig.SelectionWindowSpacing;

        // Info window text.
        var infoChildrenElements = new List<UIElement>();
        for (int partyIndex = 0; partyIndex < _partyMembers.Length; partyIndex ++)
        {
            // Create and store components in array to be updated later.
            var infoTextData = new TextData() { Text = "" };
            var component = _textComponentFactory.Create(infoTextData, _textStyle);
            _partyTextComponents.Add(component);

            // Place UI element children.
            infoChildrenElements.Add(new UIElement()
            {
                UIComponent = component,
                Layout = new UILayout() 
                {
                    Position = new Vector2f(0, partyIndex * _spacing),
                    Size = new Vector2f(1, 1)
                },

                LocalOffset = new Vector2f(0, 0),
                Children =  [],

                RenderLayer = RenderLayer.UIText
            });
        }

        var infoWindowElement = new UIElement()
        {
            UIComponent = _spriteComponentFactory.Create(AssetType.Windows, windowStyle),
            Layout = new UILayout() 
            { 
                Position = new Vector2f(infoXLocation, infoYLocation), 
                Size = new Vector2f(infoWidth, infoHeight) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children = infoChildrenElements,

            RenderLayer = RenderLayer.UIWindow
        };

        // Selection window variables.
        // Config.
        var selectionWindowXLocation = battleWindowConfig.SelectionWindowX;
        var selectionWindowYLocation = battleWindowConfig.SelectionWindowY;
        var selectionWindowWidth = battleWindowConfig.SelectionWindowWidth;
        var selectionWindowHeight = battleWindowConfig.SelectionWindowHeight;

        _maxChoicesPerPage = battleWindowConfig.MaxChoicesPerPage;

        var selectionWindowComponent = _spriteComponentFactory.Create(AssetType.Windows, windowStyle);
        var selectionComponent = _spriteComponentFactory.Create(AssetType.ChoiceSelectionArt, selectionStyle);

        _selectionElement = new UIElement()
        {
            UIComponent = selectionComponent, 
            Layout = new UILayout() 
            { 
                Position = new Vector2f(0, 0), 
                Size = new Vector2f(selectionWindowWidth, selectionWindowHeight / _maxChoicesPerPage) 
            },

            LocalOffset = new Vector2f(0, 0),
            Children = [],

            RenderLayer = RenderLayer.UISelectionBox
        };
        _selectionWindowElement = new UIElement() 
        { 
            UIComponent = selectionWindowComponent, 
            Layout = new UILayout() 
            { 
                Position = new Vector2f(selectionWindowXLocation, selectionWindowYLocation), 
                Size = new Vector2f(selectionWindowWidth, selectionWindowHeight) 
            },

            LocalOffset = new Vector2f(0, 0),
            Children = [],

            RenderLayer = RenderLayer.UIWindow
        };

        UIElement = new UIElement()
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
                infoWindowElement,
                _selectionWindowElement
            ],

            RenderLayer = RenderLayer.UIWindow
        };
    }

    /// <summary>
    /// Updates the battle UI view based on the state of the party members.
    /// </summary>
    /// <param name="currentCharacterIndex">The index of the current character.</param>
    /// <param name="currentSelectionIndex">The index of the currently selected option.</param>
    public void Update(int currentCharacterIndex, int currentSelectionIndex)
    {
        // Update selection element location.
        var oldChoice = _currentChoice;
        _currentChoice = currentSelectionIndex;
        var choiceChange = (_currentChoice + 1) - (oldChoice + 1);

        var xLocation = _selectionElement.Layout.Position.X;
        var yLocation = _selectionElement.Layout.Position.Y;

        _selectionElement.Layout = new UILayout()
        {
            Position = new Vector2f(xLocation, yLocation + (choiceChange * _spacing)),
            Size = _selectionElement.Layout.Size
        };

        // Party information.
        for (int partyIndex = 0; partyIndex < _partyMembers.Length; partyIndex ++)
        {
            if (_partyMembers[partyIndex] != null)
            {
                var character = _partyMembers[partyIndex];
                _partyTextComponents[partyIndex].SetText($"{character.Name} HP: {character.CurrentHP} / " +
                    $"{character.GetMaxHp()}, MP: {character.CurrentMP}: {character.MaxMP}");
            }
        }

        // Choices.
        var choiceTextElements = new List<UIElement>();
        var currentCharacter = _partyMembers[currentCharacterIndex];

        // Current choice index.
        var choiceIndex = 0;

        // Base abilities.
        foreach (var id in currentCharacter.GetAbilityIDs())
        {
            TextData textData = new() { Text = _abilityRegistry.Get(id).Name };
            var textComponent = _textComponentFactory.Create(textData, _textStyle);
            choiceTextElements.Add(new UIElement()
            {
                UIComponent = textComponent,
                Layout = new UILayout()
                {
                    Position = new Vector2f(0, 0), 
                    Size = new Vector2f(1, 1),
                },

                LocalOffset = new Vector2f(0, _spacing * choiceIndex),
                Children = [],

                RenderLayer = RenderLayer.UIText
            });
            choiceIndex ++;
        }

        // Ability sets.
        foreach (var keyValuePair in currentCharacter.GetAbilitySetIDs())
        {
            var abilitySetId = keyValuePair.Key;
            TextData textData = new() { Text = _abilitySetRegistry.Get(abilitySetId).Name };
            var textComponent = _textComponentFactory.Create(textData, _textStyle);
            choiceTextElements.Add(new UIElement()
            {
                UIComponent = textComponent,
                Layout = new UILayout()
                {
                    Position = new Vector2f(0, 0), 
                    Size = new Vector2f(1, 1),
                },

                LocalOffset = new Vector2f(0, _spacing * choiceIndex),
                Children = [],

                RenderLayer = RenderLayer.UIText
            });
            choiceIndex ++;
        }

        // Additional battle options.
        if (_partyChoicesConfig.ItemsEnabled)
        {
            TextData textData = new() { Text = _partyChoicesConfig.ItemsText };
            var textComponent = _textComponentFactory.Create(textData, _textStyle);
            choiceTextElements.Add(new UIElement()
            {
                UIComponent = textComponent,
                Layout = new UILayout()
                {
                    Position = new Vector2f(0, 0), 
                    Size = new Vector2f(1, 1),
                },

                LocalOffset = new Vector2f(0, _spacing * choiceIndex),
                Children = [],

                RenderLayer = RenderLayer.UIText
            });
            choiceIndex ++;
        }
        {
            
        }
        if (_partyChoicesConfig.RunAwayEnabled)
        {
            TextData textData = new() { Text = _partyChoicesConfig.RunAwayText };
            var textComponent = _textComponentFactory.Create(textData, _textStyle);
            choiceTextElements.Add(new UIElement()
            {
                UIComponent = textComponent,
                Layout = new UILayout()
                {
                    Position = new Vector2f(0, 0), 
                    Size = new Vector2f(1, 1),
                },

                LocalOffset = new Vector2f(0, _spacing * choiceIndex),
                Children = [],

                RenderLayer = RenderLayer.UIText
            });
            choiceIndex ++;
        }

        // Assign the choice text elements as children of the selection element.
        _selectionWindowElement.Children = [.. choiceTextElements, _selectionElement];
    }
}