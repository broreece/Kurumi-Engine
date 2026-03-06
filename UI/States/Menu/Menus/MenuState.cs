namespace UI.States.Menu.Menus;

using Config.Runtime.Menus;
using Config.Runtime.Windows;
using Engine.Input.Scenes;
using Game.Entities.PlayableCharacter;
using Registry.Variables;
using Save.Core;
using Save.Interfaces;
using UI.Component.Components;
using UI.Core;
using UI.Input;
using UI.Interfaces;
using UI.States.Menu.Core;
using Utils.Interfaces;

/// <summary>
/// Menu state class, menu state starts when the player presses escape on the map scene and tries to pause the game.
/// </summary>
public class MenuState : UIState, IMenuInputController {
    /// <summary>
    /// Constructor for the menu state UI object.
    /// </summary>
    /// <param name="gameUIContext">The context required for menu game UI.</param>
    /// <param name="mainMenuConfig">The main menu config object.</param>
    /// <param name="inventoryConfig">The inventory config object.</param>
    /// <param name="fileSelectorConfig">The file selector config object.</param>
    /// <param name="windowConfig">The window config object.</param>
    /// <param name="gameWindowScaleAccessor">The game window scale accessor object.</param>
    /// <param name="characterDimensionsAccessor">The character dimensions accessor object.</param>
    /// <param name="saveAssetManager">The save asset manager object.</param>
    /// <param name="saveManager">The save manager object.</param>
    /// <param name="partyInfoAccessor">The party info accessor.</param>
    /// <param name="gameVariables">The game variables object.</param>
    /// <param name="spriteIds">An array of sprite IDs for all the playable characters.</param>
    /// <param name="maxSaveFiles">The max number of save files.</param>
    /// <param name="maxPartySize">The max party size.</param>
    /// <param name="mapName">The map name.</param>
    public MenuState(IGameUIContext gameUIContext, MainMenuConfig mainMenuConfig, InventoryConfig inventoryConfig, 
        FileSelectorConfig fileSelectorConfig, WindowConfig windowConfig, IGameWindowScaleAccessor gameWindowScaleAccessor, 
        ICharacterDimensionsAccessor characterDimensionsAccessor, ISaveAssetAccessor saveAssetManager, SaveManager saveManager,
        IPartyDynamicDataAccessor partyInfoAccessor, GameVariables gameVariables, PlayableCharacter[] playableCharacters, 
        int maxSaveFiles, int maxPartySize, string mapName) {
        // Store the game context.
        this.gameUIContext = gameUIContext;

        // Assign input map.
        inputMap = new MenuInputMap(this);

        // Store variables for other menus.
        this.inventoryConfig = inventoryConfig;
        this.fileSelectorConfig = fileSelectorConfig;
        this.windowConfig = windowConfig;
        this.gameWindowScaleAccessor = gameWindowScaleAccessor;
        this.characterDimensionsAccessor = characterDimensionsAccessor;
        this.saveAssetManager = saveAssetManager;
        this.saveManager = saveManager;
        this.partyInfoAccessor = partyInfoAccessor;
        this.gameVariables = gameVariables;
        inventory = [.. partyInfoAccessor.GetInventory().Cast<IInventoryItemAccessor>()];
        this.playableCharacters = playableCharacters;
        this.maxSaveFiles = maxSaveFiles;
        this.maxPartySize = maxPartySize;
        this.mapName = mapName;

        // Load local config variables.
        int windowArtId = mainMenuConfig.GetWindowId();
        int choiceBoxArtId = mainMenuConfig.GetChoiceBoxId();
        int fontId = mainMenuConfig.GetFontId();
        int fontSize = mainMenuConfig.GetFontSize();
        int selectionWindowX = mainMenuConfig.GetSelectionWindowX();
        int selectionWindowY = mainMenuConfig.GetSelectionWindowY();
        int selectionWindowWidth = mainMenuConfig.GetSelectionWindowWidth();
        int selectionWindowHeight = mainMenuConfig.GetSelectionWindowHeight();
        int selectionWindowSpacing = mainMenuConfig.GetSelectionWindowSpacing();
        int infoWindowX = mainMenuConfig.GetInfoWindowX();
        int infoWindowY = mainMenuConfig.GetInfoWindowY();
        int infoWindowWidth = mainMenuConfig.GetInfoWindowWidth();
        int infoWindowHeight = mainMenuConfig.GetInfoWindowHeight();
        int partyWindowX = mainMenuConfig.GetPartyWindowX();
        int partyWindowY = mainMenuConfig.GetPartyWindowY();
        int partyWindowWidth = mainMenuConfig.GetPartyWindowWidth();
        int partyWindowHeight = mainMenuConfig.GetPartyWindowHeight();

        // Store window art and font file name.
        string windowArtFileName = saveAssetManager.GetWindowArtFileName(windowArtId);
        string choiceBoxArtFileName = saveAssetManager.GetChoiceSelectionFileName(choiceBoxArtId);
        string fontFileName = saveAssetManager.GetFontFileName(fontId);

        // Create menu window and options.
        components.Push(new WindowComponent(selectionWindowX, selectionWindowY, selectionWindowWidth, selectionWindowHeight,
            windowArtFileName, windowConfig, gameWindowScaleAccessor));
        menuOptions =
        [
            new("Items", MenuCommand.Items),
            new("Equipment", MenuCommand.Equipment),
            new("Skills", MenuCommand.Skills),
            new("Save", MenuCommand.Save),
            new("Load", MenuCommand.Load),
            new("Quit", MenuCommand.Quit)
        ];
        int choiceIndex = 0;
        foreach (MenuOption menuOption in menuOptions) {
            components.Push(new ListTextComponent(selectionWindowX, selectionWindowY + (selectionWindowSpacing * choiceIndex), 
                fontSize, saveAssetManager.GetFontFileName(fontId), menuOption.GetLabel()));
            choiceIndex ++;
        }

        // Create info window.
        components.Push(new WindowComponent(infoWindowX, infoWindowY, infoWindowWidth, infoWindowHeight, windowArtFileName, windowConfig,
            gameWindowScaleAccessor));
        // TODO: Load the map name here.
        // TODO: Display day and time here.

        // Create party window.
        components.Push(new WindowComponent(partyWindowX, partyWindowY, partyWindowWidth, partyWindowHeight, windowArtFileName, windowConfig,
            gameWindowScaleAccessor));

        // Load the character stats interface.
        ICharacterStatsAccessor[] characterStats = gameUIContext.GetPartyMembers();

        int characterIndex = 0;
        foreach (ICharacterStatsAccessor characterStat in characterStats) {
            if (characterStat != null) {
                // TODO: Change these line... We should use a special party window text x and y, then have a special spacing for the
                // size of each character box.
                components.Push(new ListTextComponent(partyWindowX, partyWindowY + (characterIndex * fontSize), 
                    fontSize, fontFileName, characterStat.GetName()));
                components.Push(new ListTextComponent(partyWindowX, partyWindowY + ((characterIndex + 1) * fontSize), 
                    fontSize, fontFileName, $"HP: {characterStat.GetCurrentHp()} / {characterStat.GetMaxHp()}"));
                components.Push(new ListTextComponent(partyWindowX, partyWindowY + ((characterIndex + 2) * fontSize), 
                    fontSize, fontFileName, $"MP: {characterStat.GetCurrentMp()} / {characterStat.GetMaxMp()}"));
            }
            characterIndex ++;
            // TODO: Format this so HP / MP are a bar and display statuses too.
        }

        // Create choice box here.
        choiceBox = new ChoiceBoxComponent(selectionWindowX, selectionWindowY, selectionWindowWidth, selectionWindowHeight / menuOptions.Count,
            selectionWindowSpacing, choiceBoxArtFileName, menuOptions.Count, windowConfig, gameWindowScaleAccessor);
        components.Push(choiceBox);
    }

    /// <summary>
    /// The select function.
    /// </summary>
    public void Select() {
        int choice = choiceBox.GetChoice();
        gameUIContext.PopUIStack();
        switch (menuOptions[choice].GetMenuCommand()) {
            case MenuCommand.Items:
                gameUIContext.AddUIState(new InventoryState(gameUIContext, inventoryConfig, windowConfig, gameWindowScaleAccessor, 
                    saveAssetManager, inventory));
                break;

            case MenuCommand.Equipment:
                // TODO: Open equipment.
                break;

            case MenuCommand.Skills:
                // TODO: Open skills.
                break;

            case MenuCommand.Save:
                gameUIContext.AddUIState(new FileSelectorState(gameUIContext, fileSelectorConfig, windowConfig, 
                    characterDimensionsAccessor, gameWindowScaleAccessor, playableCharacters, saveAssetManager, saveManager, 
                    partyInfoAccessor, gameVariables, true, maxSaveFiles, maxPartySize, mapName));
                break;

            case MenuCommand.Load:
                gameUIContext.AddUIState(new FileSelectorState(gameUIContext, fileSelectorConfig, windowConfig,
                    characterDimensionsAccessor, gameWindowScaleAccessor, playableCharacters, saveAssetManager, saveManager,
                    partyInfoAccessor, gameVariables, false, maxSaveFiles, maxPartySize, mapName));
                break;

            case MenuCommand.Quit:
                // TODO: Open quit.
                break;

            default:
                break;
        }
        // Close after opening sub menu.
        Close();
    }

    /// <summary>
    /// Function that moves up.
    /// </summary>
    public void MoveUp() {
        choiceBox.DecrementChoice();
    }

    /// <summary>
    /// Function that moves down.
    /// </summary>
    public void MoveDown() {
        choiceBox.IncrementChoice();
    }

    /// <summary>
    /// The cancel function.
    /// </summary>
    public void Cancel() {
        gameUIContext.PopUIStack();
        Close();
    }

    /// <summary>
    /// The escape function.
    /// </summary>
    public void Escape() {
        gameUIContext.PopUIStack();
        Close();
    }

    /// <summary>
    /// Function used to close the menu state.
    /// </summary>
    protected override void Close() {
        // TODO: Implement closing animation, lock controls etc whatever is needed here.
        closed = true;
    }

    // Stored game variables.
    private readonly IGameUIContext gameUIContext;
    private readonly string mapName;

    // Stored variables for other menus.
    private readonly InventoryConfig inventoryConfig;
    private readonly FileSelectorConfig fileSelectorConfig;
    private readonly WindowConfig windowConfig;
    private readonly IGameWindowScaleAccessor gameWindowScaleAccessor;
    private readonly ICharacterDimensionsAccessor characterDimensionsAccessor;
    private readonly ISaveAssetAccessor saveAssetManager;
    private readonly SaveManager saveManager;
    private readonly List<IInventoryItemAccessor> inventory;
    private readonly PlayableCharacter[] playableCharacters;
    private readonly IPartyDynamicDataAccessor partyInfoAccessor;
    private readonly GameVariables gameVariables;
    private readonly int maxSaveFiles, maxPartySize;

    // Components.
    private readonly ChoiceBoxComponent choiceBox;
    // TODO: Implement info text here.
    //private readonly ListTextComponent infoText;

    private readonly List<MenuOption> menuOptions;
}
