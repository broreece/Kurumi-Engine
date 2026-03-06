namespace UI.States.Menu.Menus;

using Config.Runtime.Menus;
using Config.Runtime.Windows;
using Engine.Input.Scenes;
using Game.Entities.PlayableCharacter;
using Game.Map.Elements;
using Registry.Variables;
using Save.Core;
using Save.Interfaces;
using UI.Component.Components;
using UI.Core;
using UI.Input;
using UI.Interfaces;
using Utils.Strings;
using SFML.Graphics;
using SFML.System;

/// <summary>
/// File selector state class, Opens during the save and load screen allowing selection of a save file.
/// </summary>
public class FileSelectorState : UIState, IFileSelectorInputController {
    /// <summary>
    /// Constructor for the file selector UI state object.
    /// </summary>
    /// <param name="gameUIContext">The context required for menu game UI.</param>
    /// <param name="fileSelectorConfig">The file selector config object.</param>
    /// <param name="windowConfig">The window config object,</param>
    /// <param name="characterDimensionsAccessor">The character dimensions accessor object.</param>
    /// <param name="gameWindowScalesAccessor">The game window scales accessor object.</param>
    /// <param name="playableCharacters">The array for all playable characters.</param>
    /// <param name="saveAssetManager">The save asset manager object.</param>
    /// <param name="saveManager">The save manager object.</param>
    /// <param name="partyInfoAccessor">The party info accessor object.</param>
    /// <param name="gameVariables">The game variables object.</param>
    /// <param name="saving">If the file selector is in a saving state or loading state.</param>
    /// <param name="saveFiles">The maximum number of saves.</param>
    /// <param name="maxPartySize">The max party size.</param>
    /// <param name="mapName">The map name.</param>
    public FileSelectorState(IGameUIContext gameUIContext, FileSelectorConfig fileSelectorConfig,  WindowConfig windowConfig, 
        ICharacterDimensionsAccessor characterDimensionsAccessor, IGameWindowScaleAccessor gameWindowScalesAccessor,
        PlayableCharacter[] playableCharacters, ISaveAssetAccessor saveAssetManager, SaveManager saveManager, 
        IPartyDynamicDataAccessor partyInfoAccessor, GameVariables gameVariables, bool saving, int saveFiles, int maxPartySize, 
        string mapName) {
            
        // Assign input map.
        inputMap = new FileSelectorInputMap(this);

        // Store variables.
        this.gameUIContext = gameUIContext;
        this.saveAssetManager = saveAssetManager;
        this.saveManager = saveManager;
        this.partyInfoAccessor = partyInfoAccessor;
        this.gameVariables = gameVariables;
        this.saving = saving;
        this.saveFiles = saveFiles;
        this.maxPartySize = maxPartySize;
        this.mapName = mapName;
        // TODO: I think we should interface the playable characters, we just need the sprite IDs and the saveable character interface.
        this.playableCharacters = playableCharacters;
        fileOffset = 0;

        // Store sprite IDs.
        spriteIds = new int[playableCharacters.Length];
        int index = 0;
        foreach (PlayableCharacter playableCharacter in playableCharacters) {
            spriteIds[index] = playableCharacter.GetFieldSpriteId();
            index ++;
        }

        /// Load config.
        int fileSelectionWindowId = fileSelectorConfig.GetFileSelectorWindowId();
        int fileSelectionChoiceBoxId = fileSelectorConfig.GetFileSelectorChoiceBoxId();
        int fontId = fileSelectorConfig.GetFontId();
        int fontSize = fileSelectorConfig.GetFontSize();
        int warningChoiceBoxWidth = fileSelectorConfig.GetWarningChoiceWindowWidth();
        int warningChoiceBoxHeight = fileSelectorConfig.GetWarningChoiceWindowHeight();
        int warningChoiceBoxX = fileSelectorConfig.GetWarningChoiceWindowX();
        int warningChoiceBoxY = fileSelectorConfig.GetWarningChoiceWindowY();
        string warningMessage = fileSelectorConfig.GetWarningMessage();
        int warningMessageBoxWidth = fileSelectorConfig.GetWarningMessageWindowWidth();
        int warningMessageBoxHeight = fileSelectorConfig.GetWarningMessageWindowHeight();
        int warningMessageBoxX = fileSelectorConfig.GetWarningMessageWindowX();
        int warningMessageBoxY = fileSelectorConfig.GetWarningMessageWindowY();
        maxFilesOneScreen = fileSelectorConfig.GetMaxFilesOneScreen();
        int fileMessageWidth = fileSelectorConfig.GetFileMessageWindowWidth();
        int fileMessageHeight = fileSelectorConfig.GetFileMessageWindowHeight();
        int fileMessageX = fileSelectorConfig.GetFileMessageWindowX();
        int fileMessageY = fileSelectorConfig.GetFileMessageWindowY();
        string message = saving ? fileSelectorConfig.GetSaveMessage() : fileSelectorConfig.GetLoadMessage();
        
        // Store window art and font file name.
        string windowArtFileName = saveAssetManager.GetWindowArtFileName(fileSelectionWindowId);
        string choiceBoxArtFileName = saveAssetManager.GetChoiceSelectionFileName(fileSelectionChoiceBoxId);
        string fontArtFileName = saveAssetManager.GetFontFileName(fontId);

        // File message.
        components.Push(new WindowComponent(fileMessageX, fileMessageY, fileMessageWidth, fileMessageHeight, 
            windowArtFileName, windowConfig, gameWindowScalesAccessor));
        components.Push(new PageTextComponent(fileMessageX, fileMessageY, fontSize, fontArtFileName,
            PageGenerator.TurnTextIntoPages(message, windowConfig.GetMaxLinesPerWindow())));

        // Warning message.
        string[] choices = ["Yes", "No"];
        fileWarningMessageWindow = new WindowComponent(warningMessageBoxX, warningMessageBoxY, warningChoiceBoxWidth, warningChoiceBoxHeight,
            windowArtFileName, windowConfig, gameWindowScalesAccessor);
        fileWarningChoiceWindow = new WindowComponent(warningChoiceBoxX, warningChoiceBoxY, warningChoiceBoxWidth, warningChoiceBoxHeight,
            saveAssetManager.GetWindowArtFileName(fileSelectionWindowId), windowConfig, gameWindowScalesAccessor);
        warningChoiceBox = new ChoiceBoxComponent(warningChoiceBoxX, warningChoiceBoxY, warningMessageBoxWidth, warningMessageBoxHeight,
            fontSize, choiceBoxArtFileName, choices.Length, windowConfig, gameWindowScalesAccessor);
        // Warning message text.
        fileWarningText = new PageTextComponent(warningChoiceBoxX, warningChoiceBoxY, fontSize, fontArtFileName, 
            PageGenerator.TurnTextIntoPages(warningMessage, windowConfig.GetMaxLinesPerWindow()));
        fileWarningChoiceText = new ListTextComponent[choices.Length];
        int lineIndex = 0;
        foreach (string choice in choices) {
            fileWarningChoiceText[lineIndex] = new ListTextComponent(warningChoiceBoxX, warningChoiceBoxY + (fontSize * lineIndex), fontSize,
                fontArtFileName, choice);
            lineIndex ++;
        }

        /// Create files windows.
        // Load window information.
        int scaledFileMessageHeight = (int) ((float) gameWindowScalesAccessor.GetHeight() * (float) ((float) fileMessageHeight 
            / (float) 100));
        saveWindowXLocation = fileMessageX;
        startSaveWindowYLocation = scaledFileMessageHeight;
        int saveWindowWidth = fileMessageWidth;
        int saveWindowHeight = (100 - fileMessageHeight) / maxFilesOneScreen;
        scaledSaveWindowHeight = (int) ((float) gameWindowScalesAccessor.GetHeight() * (float) ((float) saveWindowHeight / 100));

        // Load character sprite information.
        characterWidth = characterDimensionsAccessor.GetCharacterWidth();
        characterHeight = characterDimensionsAccessor.GetCharacterHeight();
        widthScale = gameWindowScalesAccessor.GetWidthScale();
        float heightScale = gameWindowScalesAccessor.GetHeightScale();
        characterScale = new Vector2f (widthScale, heightScale);

        // For each save load the party sprites.
        for (int saveIndex = 0; saveIndex < maxFilesOneScreen; saveIndex ++) {
            // Window components.
            int saveWindowYLocation = startSaveWindowYLocation + (scaledSaveWindowHeight * saveIndex);
            components.Push(new WindowComponent(saveWindowXLocation, saveWindowYLocation, saveWindowWidth, saveWindowHeight,
                windowArtFileName, windowConfig, gameWindowScalesAccessor));
        }

        // Choice box.
        fileChoiceBox = new ChoiceBoxComponent(saveWindowXLocation, startSaveWindowYLocation, saveWindowWidth, saveWindowHeight,
            scaledSaveWindowHeight, choiceBoxArtFileName, saveFiles, windowConfig, gameWindowScalesAccessor);
        components.Push(fileChoiceBox);

        // Save file text.
        // TODO: Implement here.

        // Sprites component.
        spritesComponent = new SpritesComponent();
        SetCharacterSprites();
        components.Push(spritesComponent);
    }

    /// <summary>
    /// The select function.
    /// </summary>
    public void Select() {
        int choice = fileChoiceBox.GetChoice();

        // TODO: Open a warning choice box somewhere on screen that says "Are you sure you want to overwrite this save file?"
        saveManager.SetActiveSlot(choice + 1);
        if (saving) {
            saveManager.Save(partyInfoAccessor, playableCharacters, gameVariables, mapName);
        }
        else {
            // TODO: Implement load here.
        }

        Close();
    }

    /// <summary>
    /// Function that moves up.
    /// </summary>
    public void MoveUp() {
        int oldChoice = fileChoiceBox.GetChoice();
        fileChoiceBox.DecrementChoice();
        int currentChoice = fileChoiceBox.GetChoice();
        if (currentChoice < (saveFiles - 2)) {
            fileOffset = fileOffset > 0 ? fileOffset - 1 : 0;
        }
        if (oldChoice != currentChoice) {
            SetCharacterSprites();
        }
    }

    /// <summary>
    /// Function that moves down.
    /// </summary>
    public void MoveDown() {
        int oldChoice = fileChoiceBox.GetChoice();
        fileChoiceBox.IncrementChoice();
        int currentChoice = fileChoiceBox.GetChoice();
        if (currentChoice > 1 && currentChoice < (saveFiles - 1)) {
            fileOffset = currentChoice - 1;
        }
        if (oldChoice != currentChoice) {
            SetCharacterSprites();
        }
    }

    /// <summary>
    /// The cancel function.
    /// </summary>
    public void Cancel() {
        gameUIContext.PopUIStack();
        gameUIContext.OpenMainMenu();
        Close();
    }

    /// <summary>
    /// Function used to close the file selector state.
    /// </summary>
    protected override void Close() {
        // TODO: Implement closing animation, lock controls etc whatever is needed here.
        closed = true;
    }

    /// <summary>
    /// Function used to update the character save file sprites component.
    /// </summary>
    private void SetCharacterSprites() {
        spritesComponent.ClearList();
        for (int saveIndex = 0; saveIndex < maxFilesOneScreen; saveIndex ++) {
            int saveWindowYLocation = startSaveWindowYLocation + (scaledSaveWindowHeight * saveIndex);
            int[] sprites = saveManager.GetPartiesSprites(saveIndex + fileOffset, maxPartySize, spriteIds);
            int spriteIndex = 0;
            foreach (int sprite in sprites) {
                Texture characterTexture = new(saveAssetManager.GetCharacterFieldSheetFileName(sprite));
                Sprite characterSprite = new(characterTexture, new IntRect(0, (int) Direction.South * characterHeight, characterWidth, characterHeight))
                {
                    Scale = characterScale,
                    Position = new Vector2f(saveWindowXLocation + (spriteIndex * (characterWidth * widthScale)), saveWindowYLocation)
                };
                spritesComponent.AddSprite(characterSprite);
                spriteIndex ++;
            }
        }
    }

    // State variables.
    private readonly IGameUIContext gameUIContext;
    private readonly bool saving;
    private readonly int saveFiles, maxFilesOneScreen;
    private int fileOffset;

    // Save manager and save variables.
    private readonly SaveManager saveManager;
    private readonly IPartyDynamicDataAccessor  partyInfoAccessor;
    private readonly GameVariables gameVariables;
    private readonly string mapName;

    // Character sprite variables.
    private readonly int maxPartySize, characterHeight, characterWidth, saveWindowXLocation, startSaveWindowYLocation,
        scaledSaveWindowHeight;
    private readonly float widthScale;
    private readonly Vector2f characterScale;
    private readonly ISaveAssetAccessor saveAssetManager;
    private readonly PlayableCharacter[] playableCharacters;
    private readonly int[] spriteIds;

    // Components.
    private readonly WindowComponent fileWarningMessageWindow, fileWarningChoiceWindow;
    private readonly PageTextComponent fileWarningText;
    private readonly ChoiceBoxComponent fileChoiceBox, warningChoiceBox;
    private readonly SpritesComponent spritesComponent;
    private readonly ListTextComponent[] fileWarningChoiceText;
}