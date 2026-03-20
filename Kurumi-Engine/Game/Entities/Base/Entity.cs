namespace Game.Entities.Base;

using Game.Core;
using Game.Entities.Skills;
using Game.Entities.Status;
using Scripts.EntityScripts.Base;

/// <summary>
/// Abstract entity class, inherited by playable characters and enemies.
/// </summary>
public abstract class Entity : PresentationData {
    /// <summary>
    /// Entity parent constructor.
    /// </summary>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    /// <param name="maxHp">The max hp value of the entity.</param>
    /// <param name="spriteId">The entity's battle sprite id.</param>
    /// <param name="stats">The entities stats.</param>
    /// <param name="elementalResistances">The entities elemental resistances.</param>
    /// <param name="statusResistances">The entities status resistances.</param>
    /// <param name="baseAbilities">The entities base abilities.</param>
    protected Entity(string name, string description, int maxHp, int spriteId, int[] stats, int[] elementalResistances, 
        int[] statusResistances, List<Ability> baseAbilities) 
        : base(name, description, spriteId) {
        currentHp = maxHp;
        this.maxHp = maxHp;
        this.stats = stats;
        this.elementalResistances = elementalResistances;
        this.statusResistances = statusResistances;
        this.baseAbilities = baseAbilities;
        statuses = [];
    }

    /// <summary>
    /// Adds a status from the database onto the entities statuses.
    /// </summary>
    /// <param name="newStatus">The new status to be added to the entity.</param>
    public void AddStatus(Status newStatus) {
        statuses.Add(newStatus);
    }
    
    /// <summary>
    /// Function that clears all statuses from an entity.
    /// </summary>
    public void ClearStatuses() {
        statuses = [];
    }

    /// <summary>
    /// Removes a status from the list of statuses.
    /// </summary>
    /// <param name="status">The status to be removed from the entity.</param>
    public void RemoveStatus(Status status) {
        statuses.Remove(status);
    }

    /// <summary>
    /// Getter for the current hp of the character.
    /// </summary>
    /// <returns>The current hp of the character.</returns>
    public int GetCurrentHp() {
        return currentHp;
    }

    /// <summary>
    /// Setter for the current hp of the character.
    /// </summary>
    /// <param name="newHp">Sets the current hp of the character.</param>
    public void SetCurrentHP(int newHp) {
        if (newHp > maxHp) {
            currentHp = maxHp;
        }
        else if (newHp < 1) {
            currentHp = 0;
        }
        else {
            currentHp = newHp;
        }
    }

    /// <summary>
    /// Getter for the entities max hp value.
    /// </summary>
    /// <returns>The max hp value of the entity.</returns>
    public int GetMaxHp() {
        return maxHp;
    }

    /// <summary>
    /// Setter for the max hp of the character.
    /// </summary>
    /// <param name="newMaxHp">Sets the max hp of the character.</param>
    public void SetMaxHp(int newMaxHp) {
        maxHp = newMaxHp;
    }

    /// <summary>
    /// Getter for the entities stats.
    /// </summary>
    /// <returns>The stats of the entity.</returns>
    public int[] GetStats() {
        return stats;
    }

    /// <summary>
    /// Getter for a specific entity stat.
    /// </summary>
    /// <param name="index">The index of the stat in the stats array.</param>
    /// <returns>The specified stat in the stats array.</returns>
    public virtual int GetStat(int index) {
        int sumStats = stats[index];
        foreach (Status status in statuses) {
            sumStats = (sumStats / 100) * status.GetStat(index);
        }
        return sumStats;
    }

    /// <summary>
    /// Setter for the entities stats.
    /// </summary>
    /// <param name="newStats">The new array representing the entities stats.</param>
    public void SetStats(int[] newStats) {
        stats = newStats;
    }

    /// <summary>
    /// Getter for the entities elemental resistances.
    /// </summary>
    /// <returns>The elemental resistances of the entity.</returns>
    public int[] GetElementalResistances() {
        return elementalResistances;
    }

    /// <summary>
    /// Setter for the entities elemental resistances.
    /// </summary>
    /// <param name="newElementalResistances">The new array representing the entities elemental resistances.</param>
    public void SetElementalResistances(int[] newElementalResistances) {
        elementalResistances = newElementalResistances;
    }

    /// <summary>
    /// Getter for the entities status resistances.
    /// </summary>
    /// <returns>The status resistances of the entity.</returns>
    public int[] GetStatusResistances() {
        return statusResistances;
    }

    /// <summary>
    /// Setter for the entities status resistances.
    /// </summary>
    /// <param name="newStatusResistances">The new array representing the entities status resistances.</param>
    public void SetStatusResistances(int[] newStatusResistances) {
        statusResistances = newStatusResistances;
    }

    /// <summary>
    /// Abstract getter for the entities base abilities. Based on entity type will change conditions.
    /// </summary>
    /// <returns>The abilities of the entity.</returns>
    public abstract List<Ability> GetBaseAbilities();

    /// <summary>
    /// Abstract getter for the entities base ability.
    /// </summary>
    /// <param name="index">The index of the base ability.</param>
    /// <returns>The specified base ability.</returns>
    public abstract Ability GetBaseAbility(int index);

    /// <summary>
    /// Function that loads a specific base abilities linked entity script.
    /// </summary>
    /// <param name="abilityIndex">The index of the ability being loaded.</param>
    /// <returns>The linked entity script to the ability.</returns>
    public EntityScript GetBaseAbilityScript(int abilityIndex) {
        return baseAbilities[abilityIndex].GetScript();
    }

    /// <summary>
    /// Getter for the entities statuses.
    /// </summary>
    /// <returns>The statuses of the entity.</returns>
    public List<Status> GetStatuses() {
        return statuses;
    }

    protected int currentHp, maxHp;
    protected int[] stats, elementalResistances, statusResistances;
    protected List<Ability> baseAbilities;
    protected List<Status> statuses;
}
