namespace Game.Entities.Status;

using Game.Core;
using Game.Entities.Base;
using Game.Entities.Skills;
using Save.Interfaces;

/// <summary>
/// The status class. Entities can have assigned statuses that edit their attributes.
/// Inherits from entity element.
/// </summary>
public class Status : EntityElement, IIDAccessor {
    /// <summary>
    /// Constructor for the status element object. 
    /// </summary>
    /// <param name="name">The name of the nameable element.</param>
    /// <param name="description">The description of the nameable element.</param>
    /// <param name="spriteId">The sprite ID of the nameable element.</param>
    /// <param name="turnEffectSpriteId">The turn effect sprite of the status.</param>
    /// <param name="maxTurns">The maximum turns that the status can last.</param>
    /// <param name="priority">The priority of the status, represented by enum.</param>
    /// <param name="accuracyModifier">The accuracy modifier for the entity element.</param>
    /// <param name="evasionModifier">The evasion modifier for the entity element.</param>
    /// <param name="canAct">If the entity with the status can act.</param>
    /// <param name="cureAtEnd">If the status is cured at the end of a battle.</param>
    /// <param name="statModifiers"> The modifiers for the entities stats the status provides.</param>
    /// <param name="elementModifiers"> The modifiers for the entities elements the status provides.</param>
    /// <param name="turnScript">The turn script for the entity element.</param>
    /// <param name="sealedSkills">The list of sealed skills of the entity element.</param>
    /// <param name="sealedAbilities">The list of sealed abilities of the entity element.</param>
    /// <param name="addedAbilities">The list of added abilities of the entity element.</param>
    public Status(string name, string desc, int spriteId, int id, int turnEffectSpriteId, int maxTurns, int priority, 
        int accuracyModifier, int evasionModifier, bool canAct, bool cureAtEnd, int[] statModifiers, 
        int[] elementModifiers, string turnScript, List<Skill> sealedSkills, List<Ability> sealedAbilities, 
        List<Ability> addedAbilities) 
        : base(turnEffectSpriteId, accuracyModifier, evasionModifier, statModifiers, elementModifiers, turnScript, 
        sealedSkills, sealedAbilities, addedAbilities) {
        
        PresentationData = new(name, desc, spriteId);
        this.id = id;
        turnCount = 0;
        this.maxTurns = maxTurns;
        this.priority = (Priority) priority;
        this.canAct = canAct;
        this.cureAtEnd = cureAtEnd;
    }

    /// <summary>
    /// Getter for the status's current nameable element object,.
    /// </summary>
    /// <returns>The statuses name, sprite id and description.</returns>
    public PresentationData GetPresentationData() {
        return PresentationData;
    }
    
    /// <summary>
    /// Getter for the status's id.
    /// </summary>
    /// <returns>The id of the status.</returns>
    public int GetId() {
        return id;
    }

    /// <summary>
    /// Getter for the status's current turn count.
    /// </summary>
    /// <returns>The current turn count of the status.</returns>
    public int GetTurnCount() {
        return turnCount;
    }

    /// <summary>
    /// Setter for the status's current turn count.
    /// </summary>
    /// <param name="turnCount">The current turn count of the status.</param>
    public void SetTurnCount(int turnCount) {
        this.turnCount = turnCount;
    }

    /// <summary>
    /// Getter for the status's max possible turns.
    /// </summary>
    /// <returns>The max possible turns of the status.</returns>
    public int GetMaxTurns() {
        return maxTurns;
    }

    /// <summary>
    /// Getter for the status's priority.
    /// </summary>
    /// <returns>The priority of the status.</returns>
    public Priority GetPriority() {
        return priority;
    }

    /// <summary>
    /// Getter for the if entity can act.
    /// </summary>
    /// <returns>If the entity can act.</returns>
    public bool CanAct() {
        return canAct;
    }

    /// <summary>
    /// Getter for the if entity is cured at end of battle.
    /// </summary>
    /// <returns>If the entity is cured at the end of the battle.</returns>
    public bool CureAtEnd() {
        return cureAtEnd;
    }

    private readonly int id;
    private int turnCount;
    private readonly PresentationData PresentationData;
    private readonly int maxTurns;
    private readonly Priority priority;
    private readonly bool canAct, cureAtEnd;
}