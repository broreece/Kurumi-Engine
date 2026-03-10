namespace Scenes.Battle.Core;

using Config.Runtime.Battle;
using Config.Runtime.Game;
using Config.Runtime.Windows;
using Engine.Rendering;
using Engine.Assets;
using Save.Serialization.EnemyFormationData;
using Scenes.Base;
using Scenes.Battle.Interfaces;
using States.Battle.Interfaces;
using UI.Component.Components;
using Utils.Interfaces;
using SFML.Graphics;
using SFML.System;

/// <summary>
/// The battle scene class, draws all the sprites and contains camera logic regarding the battle scene.
/// Inherits from the scene base abstract class.
/// </summary>
public class BattleScene : SceneBase, IBattleSceneView {
    /// <summary>
    /// Constructor for the battle scene object.
    /// </summary>
    /// <param name="window">The game window object.</param>
    /// <param name="gameConfig">The game config object.</param>
    /// <param name="battleBackgroundSpriteConfig">The battle background sprite config object.</param>
    /// <param name="battleSceneConfig">The battle scene config object.</param>
    /// <param name="battleWindowConfig">The battle window config object.</param>
    /// <param name="windowConfig">The window config object.</param>
    /// <param name="assetManager">The game asset manager object, used to load textures, fonts etc.</param>
    /// <param name="partyAccessor">The party accessor object.</param>
    /// <param name="enemyFormation">The enemy formation data object, the enemy formation of the current battle scene.</param>
    /// <param name="enemySpriteAccessor">The enemy sprite accessor object.</param>
    /// <param name="battleBackgroundId">The background art ID used in the battle scene.</param>
    /// <param name="backgroundMusicId">The background music played in the battle scene.</param>
    public BattleScene(GameWindow window, GameConfig gameConfig, BattleBackgroundSpriteConfig battleBackgroundSpriteConfig, 
        BattleSceneConfig battleSceneConfig, BattleWindowConfig battleWindowConfig, WindowConfig windowConfig, AssetManager assetManager, 
        IPartyAccessor partyAccessor, EnemyFormationData enemyFormation, IEnemySpriteAccessor enemySpriteAccessor, int battleBackgroundId, 
        int backgroundMusicId)
        : base(window, assetManager) {
        // Store reused config variables.
        damageDisplayLength = battleSceneConfig.GetDamageDisplayLength();
        fontId = battleWindowConfig.GetFontId();

        // Damage display variables.
        damageDisplayed = false;
        damageDisplayClock = new Clock();
        damageText = new Text();

        // Set default battle sprites to avoid null errors.
        battleBackgroundSprite = new Sprite();
        enemySprites = [];
        characterSprites = [];
        
        // Calculate scale for full size art.
        Vector2f fullScreen = new(window.GetWidth(), window.GetHeight());
        battleBackgroundScale = new(fullScreen.X / battleBackgroundSpriteConfig.GetWidth(), fullScreen.Y / battleBackgroundSpriteConfig.GetHeight());

        // Calculate scale for other sprites.
        float widthScale = window.GetWidthScale();
        float heightScale = window.GetHeightScale();
        Vector2f scale = new(widthScale, heightScale);

        // Load window config.
        int windowArtId = battleWindowConfig.GetWindowId();
        int choiceBoxId = battleWindowConfig.GetChoiceBoxId();
        string windowFileName = assetManager.GetWindowArtFileName(windowArtId);
        string choiceBoxFileName = assetManager.GetChoiceSelectionFileName(choiceBoxId);
        fontFileName = assetManager.GetFontFileName(fontId);
        fontSize = battleWindowConfig.GetFontSize();
        int statsWindowWidth = battleWindowConfig.GetInfoWindowWidth();
        int statsWindowHeight = battleWindowConfig.GetInfoWindowHeight();
        int statsWindowX = battleWindowConfig.GetInfoWindowX();
        int statsWindowY = battleWindowConfig.GetInfoWindowY();
        int choiceBoxWindowWidth = battleWindowConfig.GetSelectionWindowWidth();
        int choiceBoxWindowHeight = battleWindowConfig.GetSelectionWindowHeight();
        choiceBoxWindowX = battleWindowConfig.GetSelectionWindowX();
        choiceBoxWindowY = battleWindowConfig.GetSelectionWindowY();

        // Load the choice box options.
        string[,] infoLines = LoadInfoText(partyAccessor.GetPartyMembers());

        // Create components.
        infoWindow = new WindowComponent(statsWindowX, statsWindowY, statsWindowWidth, statsWindowHeight, windowFileName,
            windowConfig, window);
        infoText = new PageTextComponent(statsWindowX, statsWindowY, fontSize, fontFileName, infoLines);
        choiceBoxWindow = new WindowComponent(choiceBoxWindowX, choiceBoxWindowY, choiceBoxWindowWidth, choiceBoxWindowHeight,
            windowFileName, windowConfig, window);
        choiceBoxChoices = [];
        // TODO: (BSE-03) We should change the 0 to get the first healthy party member.
        UpdateChoiceBoxChoices(partyAccessor, 0);
        choiceBox = new ChoiceBoxComponent(choiceBoxWindowX, choiceBoxWindowY, choiceBoxWindowWidth, choiceBoxWindowHeight, 
            fontSize, choiceBoxFileName, choiceBoxChoices.Count, windowConfig, window);

        // Set in scene variables.
        damageDisplayed = false;

        // Load the textures and sprites using new battle information.
        Texture[] enemyTextures = new Texture[gameConfig.GetMaxEnemyFormationSize()];
        enemySprites = [];
        for (int enemyIndex = 0; enemyIndex < enemyFormation.Enemies.Count; enemyIndex ++) {
            enemyTextures[enemyIndex] = new Texture("Files\\Art\\EnemyBattleSprites\\" +
                assetManager.GetEnemyBattleSpriteFileName(enemySpriteAccessor.GetEnemySprite(enemyFormation.Enemies[enemyIndex].Id)));
            enemySprites.Add(new Sprite(enemyTextures[enemyIndex]) {
                Scale = scale,
                Position = new(enemyFormation.Enemies[enemyIndex].X, enemyFormation.Enemies[enemyIndex].Y)
            });
            enemyIndex ++;
        }

        // Load background sprite.
        Texture battleBackgroundTexture = new("Files\\Art\\BattleBackgroundArt\\" 
            + assetManager.GetBattleBackgroundFileName(battleBackgroundId));
        battleBackgroundSprite = new Sprite(battleBackgroundTexture) {
            Scale = battleBackgroundScale
        };

        // Load character sprites.
        Texture[] characterTextures = new Texture[gameConfig.GetMaxPartySize()];
        characterSprites = [];
        int[] partySprites = partyAccessor.GetPartyBattleSprites();
        for (int characterIndex = 0; characterIndex < partySprites.Length; characterIndex ++) {
            int character = partySprites[characterIndex];
            characterTextures[characterIndex] = new Texture("Files\\Art\\CharacterBattleSpriteSheets\\" +
                assetManager.GetCharacterBattleSpriteSheet(character));
            characterSprites.Add(new Sprite(characterTextures[characterIndex]) {
                Scale = scale
            });
        }

        // Load party sprites.
        int partyXPlacement = battleWindowConfig.GetPartyX();
        for (int characterIndex = 0; characterIndex < gameConfig.GetMaxPartySize(); characterIndex ++) {
            int xShift = characterIndex * partyXPlacement;
            characterSprites[characterIndex].Position = new(partyXPlacement + xShift, battleWindowConfig.GetPartyY());
        }

        // Initalize battle targeting view as null, assign after state is created.
        battleTargetingView = null;
    }
    
    /// <summary>
    /// Battle scenes update function, updates sprites used in the battle scene.
    /// </summary>
    public override void Update() {
        // Add background and component sprite.
        AddSprite(battleBackgroundSprite);
        AddSprite(infoWindow.CreateSprite());
        AddSprite(infoText.CreateSprite());
        AddSprite(choiceBoxWindow.CreateSprite());
        AddSprite(choiceBox.CreateSprite());
        foreach (ListTextComponent textComponent in choiceBoxChoices) {
            AddSprite(textComponent.CreateSprite());
        }
        
        // Apply flash to selected enemy/party member.
        RenderStates flash = new(BlendMode.Add);
        int spriteId = 0;

        // Throw exception if battle targeting view is null here.
        if (battleTargetingView == null) {
            // TODO: (HE-01) Custom exception here.
            throw new Exception();
        }

        // Draw party.
        foreach (Sprite characterSprite in characterSprites) {
            if (characterSprite != null) {
                if (battleTargetingView.IsSelected(spriteId)) {
                    flash.Texture = characterSprite.Texture;
                    AddSpriteWithState(characterSprite, flash);
                }
                else {
                    AddSprite(characterSprite);
                }
            }
            spriteId ++;
        }
        // Add enemies.
        foreach (Sprite enemySprite in enemySprites) {
            if (enemySprite != null) {
                if (battleTargetingView.IsSelected(spriteId)) {
                    flash.Texture = enemySprite.Texture;
                    AddSpriteWithState(enemySprite, flash);
                }
                else {
                    AddSprite(enemySprite);
                }
            }
            spriteId ++;
        }

        // Add damage text.
        if (damageDisplayed && damageDisplayClock.ElapsedTime.AsMilliseconds() 
            < damageDisplayLength) {
            AddSprite(damageText);
        }
    }

    /// <summary>
    /// Function used to load and then update info text.
    /// </summary>
    /// <param name="characters">The array of characters passed from the party.</param>
    public void UpdateInfoWindow(ICharacterStatsAccessor[] characters) {
        string[,] newInfo = LoadInfoText(characters);
        infoText.SetText(newInfo);
    }

    /// <summary>
    /// Function that updates character sprites based on if the enemy/party member is knocked out or selected.
    /// </summary>
    /// <param name="enemyHpValues">The HP values of the enemy.</param>
    public void UpdateCharacterSprites(int[] enemyHpValues) {
        int currentEnemyIndex = 0;
        foreach (int hp in enemyHpValues) {
            // Set a sprite to invisible if the enemy is knocked out.
            if (hp == 0) {
                enemySprites[currentEnemyIndex].Color = new Color(255, 255, 255, 0);
            }
            currentEnemyIndex ++;
        }
    }

    /// <summary>
    /// Function used to update the damage display text based on if the damage is applied to a party member and the sprite index.
    /// </summary>
    /// <param name="partyDamage">If the damage was applied to a party member.</param>
    /// <param name="spriteIndex">The index of the sprite within the correct sprite array.</param>
    /// <param name="hpChange">The HP change to be displayed.</param>
    public void UpdateDamageText(bool partyDamage, int spriteIndex, int hpChange) {
        List<Sprite> sprites = partyDamage ? characterSprites : enemySprites;
        int damageXOffset = (int) (sprites[spriteIndex].GetGlobalBounds().Width / 2);
        int damageYOffset = (int) (sprites[spriteIndex].GetGlobalBounds().Height / 2);
        int damageXLocation = (int) sprites[spriteIndex].Position.X;
        int damageYLocation = (int) sprites[spriteIndex].Position.Y;

        // Load font and create text based on hp change.
        Color color = Color.Red;
        if (hpChange < 0) {
            color = Color.Green;
        }
        Font font = new(assetManager.GetFontFileName(fontId));
        // TODO: (BSE-03) Magic number 32, change this into config please.
        damageText = new Text(hpChange.ToString(), font)
        {
            CharacterSize = 32,
            FillColor = color,
            OutlineColor = Color.Black,
            Position = new Vector2f(damageXLocation + damageXOffset, damageYLocation + damageYOffset)
        };

        damageDisplayClock.Restart();
        damageDisplayed = true;
    }
    
    /// <summary>
    /// Function used to increment the selection window choice.
    /// </summary>
    public void IncrementSelectionWindowChoice() {
        choiceBox.IncrementChoice();
    }

    /// <summary>
    /// Function used to reduce the selection window choice.
    /// </summary>
    public void ReduceSelectionWindowChoice() {
        choiceBox.DecrementChoice();
    }

    /// <summary>
    /// Function that sets the current selection window choice in the battle scene.
    /// </summary>
    /// <param name="newCurrentChoice">The new current choice.</param>
    public void SetCurrentChoice(int newCurrentChoice) {
        choiceBox.SetCurrentChoice(newCurrentChoice);
    }

    /// <summary>
    /// Function that returns the current choice on the selection window.
    /// </summary>
    /// <returns>The current choice of the battle scene.</returns>
    public int GetCurrentChoice() {
        return choiceBox.GetChoice();
    }

    /// <summary>
    /// Function that returns the length of the choices array.
    /// </summary>
    /// <returns>The length of the choices array.</returns>
    public int GetChoicesLength() {
        return choiceBox.GetNumberOfChoices();
    }

    /// <summary>
    /// Assigns a battle targeting view object to the battle scene.
    /// This function must be called after the battle state is made to prevent circular dependencies.
    /// </summary>
    /// <param name="battleTargetingView">The battle targeting view object.</param>
    public void SetBattleTargetingView(IBattleTargetingView battleTargetingView) {
        this.battleTargetingView = battleTargetingView;
    }

    /// <summary>
    /// Function used to load the selection window text based on the current characters options.
    /// </summary>
    /// <param name="partyMemberAccessor">The party member accessor object.</param>
    /// <param name="currentCharacterIndex">The character index being selected.</param>
    private void UpdateChoiceBoxChoices(IPartyMemberAccessor partyMemberAccessor, int currentCharacterIndex) {
        ICharacterSkillsNameAccessor ? character = partyMemberAccessor.GetPartyMember(currentCharacterIndex);
        choiceBoxChoices = [];
        if (character != null) {
            // Base abilities.
            List<string> options = character.GetBaseAbilityNames();
            // Skills.
            options.AddRange(character.GetSkillNames());
            // Additional options.
            // TODO: (BSE-01) Move hard coded options into external file.
            string[] hardCodedOptions = ["Items", "Run away"];
            options.AddRange(hardCodedOptions);
            
            // Loop over all options here.
            int index = 0;
            foreach (string option in options) {
                choiceBoxChoices.Add(new ListTextComponent(choiceBoxWindowX, choiceBoxWindowY + (index * fontSize), fontSize, fontFileName, 
                    option));
                index ++;
            }
        }
        else {
            // TODO: (HE-01) Custom exception here.
            throw new Exception();
        }
    }

    /// <summary>
    /// Function used to load the information text displayed in the battle scene.
    /// </summary>
    /// <param name="ICharacterStatsAccessor">The array of character stats passed from the party.</param>
    /// <returns>The information text of the party.</returns>
    private string[,] LoadInfoText(ICharacterStatsAccessor[] characters) {
        int characterIndex = 0;
        string[,] newText = new string[1, characters.Length];
        foreach (ICharacterStatsAccessor character in characters) {
            if (character != null) {
                newText[0, characterIndex] = character.GetName() +
                    " HP: " + character.GetCurrentHp().ToString() + " / " + character.GetMaxHp().ToString() +
                    " MP: " + character.GetCurrentMp().ToString() + " / " + character.GetMaxMp().ToString();
            }
            characterIndex ++;
        }
        return newText;
    }

    // Stored config variables.
    private readonly int damageDisplayLength, fontId;

    // Timer and settings for the damage display.
    private bool damageDisplayed;
    private readonly Clock damageDisplayClock;
    private Text damageText;

    // Current sprites and background scale.
    private readonly Sprite battleBackgroundSprite;
    private readonly List<Sprite> enemySprites, characterSprites;
    private readonly Vector2f battleBackgroundScale;

    // Windows used in battle.
    private readonly WindowComponent infoWindow, choiceBoxWindow;
    private readonly PageTextComponent infoText;
    private readonly ChoiceBoxComponent choiceBox;
    private List<ListTextComponent> choiceBoxChoices;

    // Stored variables used to update components.
    private readonly string fontFileName;
    private readonly int fontSize, choiceBoxWindowX, choiceBoxWindowY;

    // Assigned after construction battle targetting viewer.
    private IBattleTargetingView? battleTargetingView;
}
