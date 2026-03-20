namespace States.Battle.Core;

using Engine.Runtime;
using Engine.ScriptManager.Core;
using Game.Battle;
using Game.Entities.Base;
using Game.Entities.PlayableCharacter;
using Save.Serialization.EnemyFormationData;
using Scenes.Battle.Interfaces;
using Scripts.Base;
using Scripts.BattleScripts.Base;
using Scripts.BattleScripts.BattleScriptSteps;
using Scripts.EntityScripts.Base;
using States.Base;
using States.Battle.Exceptions;
using States.Battle.Input;
using States.Battle.Interfaces;

/// <summary>
/// The battle state class. Contains the dynamic duties of changing a battle during the battle scene.
/// </summary>
public class BattleState : StateBase, IBattleInputController, IBattleTargetingView {
    /// <summary>
    /// Constructor for the battle state object.
    /// </summary>
    /// <param name="battle">The battle object.</param>
    /// <param name="gameContext">The game context required to pass to scripts.</param>
    /// <param name="battleSceneView">The battle scene view object.</param>
    public BattleState(Battle battle, GameContext gameContext, IBattleSceneView battleSceneView, 
        BattleScriptManager battleScriptManager) : base(gameContext) {
        // Store battle, script manager and battle scene view.
        this.battle = battle;
        this.battleSceneView = battleSceneView;
        this.battleScriptManager = battleScriptManager;

        // Set actions to empty.
        actions = [];

        // Create boolean array representing if each sprite is selected and default the current target index.
        characterSprites = party.GetPartyMembers().Count(x => x != null);
        isSpriteSelected = new bool[characterSprites + battle.GetEnemiesLength()];

        // Set current target index to the number of character sprites as 0 to characterSprites.Length indicate 
        // targetting the party. Anything above characterSprites.Length will be the enemies.
        currentTargetIndex = characterSprites;
    }

    /// <summary>
    /// Overriden update function for the battle state.
    /// </summary>
    public override void Update() {}

    /// <summary>
    /// The select function. Attempts to perform an action based on the current choice/screen on.
    /// </summary>
    public void Select(){
        // TODO: (MI) Play a sound effect here.
        PlayableCharacter currentCharacter = party.GetPartyMember(currentCharacterIndex);
        int currentChoice = battleSceneView.GetCurrentChoice();
        // Check if the character opened a skill sub menu.
        currentSkillIndex = currentChoice - currentCharacter.GetBaseAbilities().Count;

        // If selected a target.
        if (inEnemySelector) {
            isSpriteSelected = new bool[characterSprites + battle.GetEnemiesLength()];
            inEnemySelector = false;
            // Create a battle script for the use ability.
            ScriptStep head = new UseAbility(currentChoice);
            BattleScript battleScript = new("Ability in battle", head);
            actions.Add(new Action(currentCharacterIndex, currentTargetIndex, 
                party.GetPartyMember(currentCharacterIndex).GetStat(gameContext.GetAgilityStatIndex()),
                battleScript, enemy: false));
            battleSceneView.SetCurrentChoice(0);
            // Check if it's enemies turn now.
            if (currentCharacterIndex >= characterSprites - 1) {
                ConductEnemyPhase();
                ExecuteActions();
                // After enemies turn increment turn count.
                currentTurn ++;
            }
            else {
                currentCharacterIndex ++;
            }
        }

        // Open skill sub menu.
        else if (currentSkillIndex >= 0 && currentSkillIndex < currentCharacter.GetSkills().Count) {
            // TODO: (BSE) Open sub menu.
        }

        // Hard coded inventory placement.
        else if (currentChoice == battleSceneView.GetChoicesLength() - 2) {
            // TODO: (BSE) Open inventory sub menu.
        }

        // TODO: (BSE) Hard code run away command here.

        // If an ability was used.
        else {
            isSpriteSelected = new bool[characterSprites + battle.GetEnemiesLength()];

            // Check if target selected before is dead, if so find nearest target.
            bool roundingUp = true;
            while (TargetIsDead(currentTargetIndex)) {
                while (roundingUp) {
                    if (currentTargetIndex == characterSprites + battle.GetEnemiesLength() - 1) {
                        roundingUp = false;
                    }
                    currentTargetIndex ++;
                }
                currentTargetIndex --;
            }

            isSpriteSelected[currentTargetIndex] = true;
            inEnemySelector = true;
        }
    }

    /// <summary>
    /// The cancel function. Attempts to cancel and move back in the battle scene state.
    /// </summary>
    public void Cancel() {
        // TODO: (BSE) Implement a check here if it's a submenu or main menu.
        // If the player is currently selecting an enemy.
        if (inEnemySelector) {
            inEnemySelector = false;
        }

        // If on the main menu and on a character that isn't the first character.
        else if (currentCharacterIndex > 0) {
            currentCharacterIndex --;
            battleSceneView.SetCurrentChoice(0);
            actions.RemoveAt(actions.Count - 1);
        }

        // TODO: (MI) Play a sound effect here.
    }

    /// <summary>
    /// Function that attempts to move the selected option right.
    /// </summary>
    public void MoveRight() {
        if (inEnemySelector) {
            int previousTargetIndex = currentTargetIndex;
            currentTargetIndex = currentTargetIndex == characterSprites + battle.GetEnemiesLength() - 1
                ? characterSprites + battle.GetEnemiesLength() - 1 : currentTargetIndex + 1;
                    
            // Check if current target is dead, if so find the best next target.
            while (TargetIsDead(currentTargetIndex)) {
                // Check if limit is reached, if so go back to previous index.
                if (currentTargetIndex == characterSprites + battle.GetEnemiesLength() - 1) {
                    currentTargetIndex = previousTargetIndex;
                }
                else {
                    currentTargetIndex ++;
                }
            }

            isSpriteSelected = new bool[characterSprites + battle.GetEnemiesLength()];
            isSpriteSelected[currentTargetIndex] = true;
            battleSceneView.UpdateCharacterSprites(battle.GetEnemiesHp());
        }
    }

    /// <summary>
    /// Function that attempts to move the selected option left.
    /// </summary>
    public void MoveLeft() {
        if (inEnemySelector) {
            int previousTargetIndex = currentTargetIndex;
            currentTargetIndex = currentTargetIndex == 0 ? 0 : currentTargetIndex - 1;

            // Check if current target is dead, if so find the best next target.
            while (TargetIsDead(currentTargetIndex)) {
                // Check if limit is reached, if so go back to previous index.
                if (currentTargetIndex == -1) {
                    currentTargetIndex = previousTargetIndex;
                }
                else {
                    currentTargetIndex --;
                }
            }

            isSpriteSelected = new bool[characterSprites + battle.GetEnemiesLength()];
            isSpriteSelected[currentTargetIndex] = true;
            battleSceneView.UpdateCharacterSprites(battle.GetEnemiesHp());
        }
    }

    /// <summary>
    /// Function that moves the selected option up once.
    /// </summary>
    public void MoveUp() {
        if (!inEnemySelector) {
            battleSceneView.ReduceSelectionWindowChoice();
        }
    }

    /// <summary>
    /// Function that moves the selected option down once.
    /// </summary>
    public void MoveDown() {
        if (!inEnemySelector) {
            battleSceneView.IncrementSelectionWindowChoice();
        }
    }

    /// <summary>
    /// Function that checks if a specified sprite is selected.
    /// </summary>
    /// <param name="spriteIndex">The index in the sprite array.</param>
    /// <returns>If the specified sprite is highlighted.</returns>
    public bool IsSelected(int spriteIndex) {
        return isSpriteSelected[spriteIndex];
    }

    /// <summary>
    /// Function used to kill a specified enemy in the battle scene.
    /// </summary>
    /// <param name="enemyId">The provided enemy id to be killed.</param>
    public void KillEnemy(int enemyId) {
        battle.SetEnemyHp(enemyId, 0);
    }

    /// <summary>
    /// Function used to get actions that the enemies will conduct.
    /// </summary>
    private void ConductEnemyPhase() {
        int characterId = 0;
        foreach (EnemyFormationEnemyData enemyFormationData in battle.GetEnemyFormationEnemyData()) {
            List<BattleScriptData> enemyScripts = enemyFormationData.BattleScripts;
            foreach (BattleScriptData enemyScript in enemyScripts) {
                int startTurn = enemyScript.StartTurn;
                int frequency = enemyScript.Frequency;
                // If the script's frequency is reached.
                if (currentTurn == startTurn || (currentTurn > startTurn && (currentTurn + 1) % frequency == 0)) {
                    int speed = battle.GetEnemyStat(enemyFormationData.Id, gameContext.GetAgilityStatIndex());
                    int target = enemyScript.Target;
                    actions.Add(new Action(characterId, target, speed, 
                        battleScriptManager.LoadBattleScript(enemyScript.Script - 1), true));
                }
            }
            characterId ++;
        }
    }

    /// <summary>
    /// Performs all queued actions and empties the queue.
    /// </summary>
    private void ExecuteActions() {
        List<Action> sortedList = [.. actions.OrderByDescending(x => x.GetSpeed())];
        foreach (Action action in sortedList) {
            actionCharacterId = action.GetCharacterId();
            actionEnemy = action.IsEnemy();
            actionTargetId = action.GetTargetId();
            // Enemy indexes need to be reduced by the number of character sprites when targeted.
            int enemyIndex = actionTargetId - characterSprites;

            // Check if the character commiting the action is still alive.
            if ((actionEnemy && battle.GetEnemyHp(actionCharacterId) > 0) || (!actionEnemy && 
                party.GetPartyMember(actionCharacterId).GetCurrentHp() > 0)) {

                // Check if the target enemy or party member is alive. Updates target ID based on the action.
                // First check if the target is above 0 or it'll cause index out of bounds errors.
                if (actionTargetId >= 0 && TargetIsDead(actionTargetId)) {
                    // Check if target is enemy or party.
                    if (actionTargetId > characterSprites) {
                        // Subtract the character sprites to get the enemy index.
                        int selectedEnemyId = actionTargetId - characterSprites;
                        if (battle.GetEnemyHp(enemyIndex) == 0) {
                            // If the enemy is dead we try to load it's "Main part" aka the torso.
                            selectedEnemyId = battle.GetEnemyFormationEnemyData(selectedEnemyId).MainPart + 
                                characterSprites;
                            // Incriment untill we find a main body part that is left over.
                            while (battle.GetEnemyHp(selectedEnemyId) == 0) {
                                selectedEnemyId ++;
                            }
                            actionTargetId = selectedEnemyId;
                        }
                    }
                    else {
                        // If the target is a party member but the party member is dead we check the first party member 
                        // then increment by 1 untill new target is found.
                        int selectedCharacterId = 0;
                        while (party.GetPartyMember(selectedCharacterId).GetCurrentHp() == 0) {
                            selectedCharacterId ++;
                        }
                        actionTargetId = selectedCharacterId;
                    }
                }
                BattleScript battleScript = action.GetBattleScript();
                battleScript.Activate(gameContext);

                // Check if any enemy main enemy body part died and if so kill all attached parts.
                if (actionTargetId >= characterSprites && battle.GetEnemyHp(enemyIndex) == 0) {
                    int enemyDataIndex = 0;
                    foreach (EnemyFormationEnemyData enemyFormationData in battle.GetEnemyFormationEnemyData()) {
                        if (enemyFormationData.MainPart == enemyIndex) {
                            battle.SetEnemyHp(enemyDataIndex, 0);
                        }
                        enemyDataIndex ++;
                    }
                    // Activate on kill scripts.
                    int onKillScriptId = battle.GetEnemyFormationEnemyData(enemyIndex).OnKillScript;
                    if (onKillScriptId > 0) {
                        BattleScript onKillScript = battleScriptManager.LoadBattleScript(onKillScriptId - 1);
                        onKillScript.Activate(gameContext);
                    }
                }

                // Make any neccesary changes to the party info window and update all sprites.
                battleSceneView.UpdateInfoWindow(party.GetPartyMembers());
                battleSceneView.UpdateCharacterSprites(battle.GetEnemiesHp());
            }
        }
        actions = [];
    }

    /// <summary>
    /// Function that activates a specified ability. Uses current action variables to determine target and user. 
    /// </summary>
    /// <param name="abilityId">The ability id that the current action user uses.</param>
    /// <exception cref="NullTargetException">Error thrown if a selected target is null.</exception>
    public void ActivateAbility(int abilityId) {
        // Load user based on if it's an enemy or not.
        Entity user = actionEnemy ? battle.GetEnemy(actionCharacterId) : party.GetPartyMember(actionCharacterId);
        Entity ? target = null;

        bool partyDamage = true;
        int spriteIndex = 0;

        // Load target.
        switch (actionTargetId) {
            case -1:
                // TODO: (BSE) Check if party member is dead and if so generate a new random number.
                Random random = new();
                int randomNumber = random.Next(0, characterSprites);
                target = party.GetPartyMember(randomNumber);
                spriteIndex = randomNumber;
                break;
            
            case -2:
                // TODO: (BSE) Implement party wide scripts here.
                break;

            case -3:
                // TODO: (BSE) Implement random enemy here.
                break;
            
            case -4:
                // TODO: (BSE) Implement enemy group scripts here.
                break;

            default:
                if (actionTargetId >= 0) {
                    // If target is an enemy.
                    if (actionTargetId >= characterSprites) {
                        int enemyId = actionTargetId - characterSprites;
                        target = battle.GetEnemy(enemyId);
                        partyDamage = false;
                        spriteIndex = enemyId;
                    }
                    // If target is party.
                    else {
                        target = party.GetPartyMember(actionTargetId);
                        spriteIndex = actionTargetId;
                    }
                }
                break;
        }
        if (target == null) {
            throw new NullTargetException("Target was not found.");
        }

        // TODO: (BSE) We should add a check here for skills as well.
        // Calculate HP change alongside activating the effect.
        int oldHp = target.GetCurrentHp();
        EntityScript entityScript = user.GetBaseAbilityScript(abilityId);
        entityScript.Activate(gameContext, user, target);
        int hpChange = oldHp - target.GetCurrentHp();

        // Update sprite view to display damage.
        battleSceneView.UpdateDamageText(partyDamage, spriteIndex, hpChange);
    }
    
    /// <summary>
    /// Checks if the selected target is dead or not.
    /// </summary>
    /// <param name="targetId">The selected target.</param>
    /// <returns>True if the target selected is dead, false otherwise.</returns>
    private bool TargetIsDead(int targetId) {
        return (targetId >= characterSprites && battle.GetEnemyHp(targetId - characterSprites) == 0) 
            || (targetId < characterSprites && party.GetPartyMember(targetId).GetCurrentHp() == 0);
    }

    /// <summary>
    /// Checks if the battle has been lost.
    /// </summary>
    /// <returns>If the lead of the party has died.</returns>
    private bool LostBattle() {
        return party.GetPartyMember(0).GetCurrentHp() == 0;
    }

    // The battle object, battle scene view and script manager.
    private readonly Battle battle;
    private readonly IBattleSceneView battleSceneView;
    private readonly BattleScriptManager battleScriptManager;

    // Reused config and variables.
    private readonly int characterSprites;

    // Selected sprite boolean array.
    private bool[] isSpriteSelected;
    
    // Dynamic variables that can be changed within battle scene.
    private int currentCharacterIndex, currentTargetIndex, currentSkillIndex, currentTurn;
    private bool inEnemySelector;

    // Variables used to store temporary values for battle scene scripts.
    private int actionCharacterId, actionTargetId;
    private bool actionEnemy;

    // List of actions to be executed in order.
    private List<Action> actions;
}
