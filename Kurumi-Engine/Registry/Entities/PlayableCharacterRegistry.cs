namespace Registry.Entities;

using Database.Core;
using Game.Entities.PlayableCharacter;
using Registry.Items;
using Registry.Names;
using Registry.Skills;
using Save.Core;

/// <summary>
/// The playable character data registry, contains data about the playable characters.
/// </summary>
public sealed class PlayableCharacterRegistry {
    /// <summary>
    /// Constructor for the playable character data registry.
    /// </summary>
    /// <param name="databaseManager">The database object that is passed.</param>
    /// <param name="skillRegistry">The skill data that is passed.</param>
    /// <param name="abilityRegistry">The ability data that is passed.</param>
    /// <param name="equipmentRegistry">The equipment data that is passed.</param>
    /// <param name="equipmentSlotNameRegistry">The equipment slot name data that is passed.</param>
    /// <param name="saveManager">The save manager object that is passed.</param>
    public PlayableCharacterRegistry(DatabaseManager databaseManager, SkillRegistry skillRegistry, 
        AbilityRegistry abilityRegistry, EquipmentRegistry equipmentRegistry, 
        EquipmentSlotNameRegistry equipmentSlotNameRegistry, SaveManager saveManager) {
        playableCharacters = saveManager.LoadPlayableCharacters(databaseManager, skillRegistry, abilityRegistry, 
            equipmentRegistry, equipmentSlotNameRegistry);
    }

    /// <summary>
    /// Getter for the playable characters data.
    /// </summary>
    /// <returns>The array of the game's playable characters.</returns>
    public PlayableCharacter[] GetPlayableCharacters() {
        return playableCharacters;
    }

    /// <summary>
    /// Getter for a specific playable character.
    /// </summary>
    /// <param name="index">The index of the desired playable character.</param>
    /// <returns>A playable character at the index.</returns>
    public PlayableCharacter GetPlayableCharacter(int index) {
        return playableCharacters[index];
    }

    /// <summary>
    /// Function used to load an array of all the playable character field sprite IDs.
    /// </summary>
    /// <returns>The array of field sprite IDs.</returns>
    public int[] GetPlayableCharacterFieldSpriteIds() {
        int[] spriteIds = new int[playableCharacters.Length];
        int index = 0;
        foreach (PlayableCharacter character in playableCharacters) {
            spriteIds[index] = character.GetFieldSpriteId();
            index ++;
        }
        return spriteIds;
    }

    // TODO: (AS-01) Implement a save feature here.

    private PlayableCharacter[] playableCharacters;
}