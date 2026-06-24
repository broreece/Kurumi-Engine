// Config.
using Config.Runtime.Battle;

// Data.
using Data.Definitions.Entities.Abilities.Core;

using Data.Runtime.Entities.Core;

// Engine.
using Engine.Assets.Base;
using Engine.Assets.Core;

using Engine.Systems.Rendering.Base;

using Engine.UI.Components.Core;
using Engine.UI.Components.Factories;
using Engine.UI.Data.Content;
using Engine.UI.Data.Content.Layout;
using Engine.UI.Data.Style;
using Engine.UI.Elements;

// Infrastructure.
using Infrastructure.Database.Base;

// External libraries.
using SFML.System;

namespace Game.UI.Views.Core;

public sealed class BattleView
{
    // Registries for abilities and skills to access names.
    private readonly Registry<AbilityDefinition> _abilityRegistry;
    private readonly Registry<NamedData> _abilitySetRegistry;

    // Party members.
    private readonly Character[] _partyMembers;

    // Cached config.
    private readonly PartyChoicesConfig _partyChoicesConfig;
    private readonly int _maxChoicesPerPage, _spacing, _selectionWindowXOffset, _selectionWindowYOffset;

    // Component factories.
    private readonly SpriteComponentFactory _spriteComponentFactory;
    private readonly TextComponentFactory _textComponentFactory;

    // Cached styles.
    private readonly TextStyle _textStyle;

    // Components and children elements.
    private readonly List<TextComponent> _partyTextComponents = [];
    private UIElement? _selectionWindowElement, _selectionElement;

    // Choice selection location variables.
    private int _currentChoice = 0;

    // Elements.
    public UIElement UIElement { get; }

    internal BattleView(
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

        _partyChoicesConfig = partyChoicesConfig;
        _spacing = battleWindowConfig.SelectionWindowSpacing;
        _selectionWindowXOffset = battleWindowConfig.SelectionWindowTextXOffset;
        _selectionWindowYOffset = battleWindowConfig.SelectionWindowTextYOffset;
        _maxChoicesPerPage = battleWindowConfig.MaxChoicesPerPage;

        _partyMembers = partyMembers;

        var windowStyle = new SpriteStyle() { SpriteArt = battleWindowConfig.WindowArtName };
        _textStyle = new TextStyle() 
        { 
            FontSize = (uint) battleWindowConfig.FontSize, 
            FontArt = battleWindowConfig.FontName 
        };

        _spriteComponentFactory = new SpriteComponentFactory(assetRegistry);
        _textComponentFactory = new TextComponentFactory(assetRegistry);

        var infoWindowElement = CreateInfoWindowElement(battleWindowConfig, windowStyle);
        CreateSelectionWindowElement(battleWindowConfig, windowStyle);

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
                _selectionWindowElement!
            ],

            RenderLayer = RenderLayer.UIWindow
        };
    }

    public void Update(int currentCharacterIndex, int currentSelectionIndex)
    {
        UpdateBattleChoices(currentCharacterIndex);
        UpdateSelection(currentSelectionIndex);
        UpdatePartyInformation();
    }
    
    private void UpdateSelection(int currentSelectionIndex)
    {
        var oldChoice = _currentChoice;
        _currentChoice = currentSelectionIndex;

        var choiceChange = _currentChoice - oldChoice;

        var position = _selectionElement!.Layout.Position;

        _selectionElement.Layout = new UILayout()
        {
            Position = new Vector2f(
                position.X, 
                position.Y + (choiceChange * _spacing)
            ),
            Size = _selectionElement.Layout.Size
        };
    }

    private void UpdatePartyInformation()
    {
        for (int partyIndex = 0; partyIndex < _partyMembers.Length; partyIndex++)
        {
            var character = _partyMembers[partyIndex];

            if (character != null)
            {
                _partyTextComponents[partyIndex].SetText(
                    $"{character.Name} HP: {character.CurrentHP} / {character.MaxHp}, " +
                    $"MP: {character.CurrentMP} / {character.MaxMP}"
                );
            }
        }
    }

    private void UpdateBattleChoices(int currentCharacterIndex)
    {
        var choiceTextElements = new List<UIElement>();
        var currentCharacter = _partyMembers[currentCharacterIndex];

        AddAbilityChoices(choiceTextElements, currentCharacter);
        AddAbilitySetChoices(choiceTextElements, currentCharacter);
        AddAdditionalChoices(choiceTextElements);

        // Assign the choice text elements as children of the selection element.
        _selectionWindowElement!.Children = [.. choiceTextElements, _selectionElement!];
    }

    private void AddAbilityChoices(List<UIElement> choiceTextElements, Character currentCharacter)
    {
        foreach (var id in currentCharacter.AbilityIDs)
        {
            choiceTextElements.Add(CreateChoiceElement(_abilityRegistry.Get(id).Name, choiceTextElements.Count));
        }
    }

    private void AddAbilitySetChoices(List<UIElement> choiceTextElements, Character currentCharacter)
    {
        foreach (var keyValuePair in currentCharacter.AbilitySetIDs)
        {
            var abilitySetId = keyValuePair.Key;
            choiceTextElements.Add(
                CreateChoiceElement(
                    _abilitySetRegistry.Get(abilitySetId).Name, 
                    choiceTextElements.Count
                )
            );
        }
    }

    private void AddAdditionalChoices(List<UIElement> choiceTextElements)
    {
        if (_partyChoicesConfig.ItemsEnabled)
        {
            choiceTextElements.Add(CreateChoiceElement(_partyChoicesConfig.ItemsText, choiceTextElements.Count));
        }

        if (_partyChoicesConfig.RunAwayEnabled)
        {
            choiceTextElements.Add(CreateChoiceElement(_partyChoicesConfig.RunAwayText, choiceTextElements.Count));
        }
    }

    private UIElement CreateChoiceElement(string text, int index)
    {
        var textComponent = _textComponentFactory.Create(new TextData() { Text = text }, _textStyle);

        return new UIElement()
        {
            UIComponent = textComponent,

            Layout = new UILayout()
            {
                Position = new Vector2f(0,0),
                Size = new Vector2f(1,1)
            },

            LocalOffset = new Vector2f(
                _selectionWindowXOffset,
                (_spacing * index) + _selectionWindowYOffset
            ),

            Children = [],

            RenderLayer = RenderLayer.UIText
        };
    }

    private void CreateSelectionWindowElement(BattleWindowConfig battleWindowConfig, SpriteStyle windowStyle)
    {
        var selectionStyle = new SpriteStyle() { SpriteArt = battleWindowConfig.ChoiceBoxArtName };

        // Reused selection window config.
        var selectionWindowWidth = battleWindowConfig.SelectionWindowWidth;
        var selectionWindowHeight = battleWindowConfig.SelectionWindowHeight;

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
                Position = new Vector2f(battleWindowConfig.SelectionWindowX, battleWindowConfig.SelectionWindowY), 
                Size = new Vector2f(selectionWindowWidth, selectionWindowHeight) 
            },

            LocalOffset = new Vector2f(0, 0),
            Children = [],

            RenderLayer = RenderLayer.UIWindow
        };
    }

    private UIElement CreateInfoWindowElement(BattleWindowConfig battleWindowConfig, SpriteStyle windowStyle) {
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

                LocalOffset = new Vector2f(
                    battleWindowConfig.InfoWindowTextXOffset, 
                    battleWindowConfig.InfoWindowTextYOffset
                ),
                Children =  [],

                RenderLayer = RenderLayer.UIText
            });
        }

        return new UIElement()
        {
            UIComponent = _spriteComponentFactory.Create(AssetType.Windows, windowStyle),
            Layout = new UILayout() 
            { 
                Position = new Vector2f(battleWindowConfig.InfoWindowX, battleWindowConfig.InfoWindowY), 
                Size = new Vector2f(battleWindowConfig.InfoWindowWidth, battleWindowConfig.InfoWindowHeight) 
            },
            
            LocalOffset = new Vector2f(0, 0),
            Children = infoChildrenElements,

            RenderLayer = RenderLayer.UIWindow
        };
    }
}