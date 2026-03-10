namespace Engine.Assets;

using UI.Interfaces;
using Utils.Exceptions;
using System.Text.Json;

/// <summary>
/// The asset manager class, loads and stores string file name locations for assets for dynamic loading in scenes.
/// </summary>
public sealed class AssetManager : ISaveAssetAccessor {
    /// <summary>
    /// Constructor for the asset manager class.
    /// </summary>
    /// <param name="registryPath">The path of the json registry file.</param>
    public AssetManager(string registryPath) {
        // Load json file.
        string json = File.ReadAllText(registryPath);
        var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json) ?? throw new MissingJsonFileException();

        // Store file names in array.
        animatedTileSheetFileNames = data["AnimatedTileSpriteSheets"].Values.ToArray();
        battleBackgroundFileNames = data["BattleBackgroundArt"].Values.ToArray();
        characterBattleSpriteSheetFileNames = data["CharacterBattleSpriteSheets"].Values.ToArray();
        characterFieldSheetFileNames = data["CharacterFieldSpriteSheets"].Values.ToArray();
        enemyBattleSpriteFileNames = data["EnemyBattleSprites"].Values.ToArray();
        // TODO: (MSAC-01) Create actor field sheet file names.
        //actorFieldSheetFileNames = data["ActorFieldSpriteSheets"].Values.ToArray();
        fontFileNames = data["Fonts"].Values.ToArray();
        mapBackgroundFileNames = data["MapBackgroundArt"].Values.ToArray();
        tileSheetFileNames = data["TileSpriteSheets"].Values.ToArray();
        windowArtFileNames = data["Windows"].Values.ToArray();
        choiceSelectionFileNames = data["ChoiceSelectionArt"].Values.ToArray();
    }

    /// <summary>
    /// Getter for a specified animated tile sheet file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified animated tile sheet file name.</returns>
    public string GetAnimatedTileSheetFileName(int index) {
        return animatedTileSheetFileNames[index];
    }

    /// <summary>
    /// Getter for a specified battle background file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified battle background file name.</returns>
    public string GetBattleBackgroundFileName(int index) {
        return battleBackgroundFileNames[index];
    }

    /// <summary>
    /// Getter for a specified character battle sprite sheet file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified character battle sprite sheet file name.</returns>
    public string GetCharacterBattleSpriteSheet(int index) {
        return characterBattleSpriteSheetFileNames[index];
    }

    /// <summary>
    /// Getter for a specified character field sprite sheet file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified character field sprite sheet file name.</returns>
    public string GetCharacterFieldSheetFileName(int index) {
        return characterFieldSheetFileNames[index];
    }

    /// <summary>
    /// Getter for a specified enemy battle sprite file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified enemy battle sprite file name.</returns>
    public string GetEnemyBattleSpriteFileName(int index) {
        return enemyBattleSpriteFileNames[index];
    }

    // TODO: (MSAC-01) Re-add the getter for actor file sheet names.

    /// <summary>
    /// Getter for a specified font file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified font file name.</returns>
    public string GetFontFileName(int index) {
        return fontFileNames[index];
    }

    /// <summary>
    /// Getter for a specified map background file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified map background file name.</returns>
    public string GetMapBackgroundFileName(int index) {
        return mapBackgroundFileNames[index];
    }

    /// <summary>
    /// Getter for a specified tile sheet file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified tile sheet file name.</returns>
    public string GetTileSheetFileName(int index) {
        return tileSheetFileNames[index];
    }

    /// <summary>
    /// Getter for a specified window art file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified window art file name.</returns>
    public string GetWindowArtFileName(int index) {
        return windowArtFileNames[index];
    }

    /// <summary>
    /// Getter for a specified choice selection art file name.
    /// </summary>
    /// <param name="index">The index of the file name in the array.</param>
    /// <returns>A specified choice selection art file name.</returns>
    public string GetChoiceSelectionFileName(int index) {
        return choiceSelectionFileNames[index];
    }

    // TODO: (MSAC-01) Add actorFieldSheetFileNames.
    private readonly string[] battleBackgroundFileNames, characterBattleSpriteSheetFileNames, characterFieldSheetFileNames,
        enemyBattleSpriteFileNames, fontFileNames, mapBackgroundFileNames, tileSheetFileNames, animatedTileSheetFileNames,
        windowArtFileNames, choiceSelectionFileNames;
}
