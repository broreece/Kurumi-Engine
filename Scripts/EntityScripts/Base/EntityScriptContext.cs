namespace Scripts.EntityScripts.Base;

using Engine.Runtime;
using Scripts.Base;
using Game.Entities.Base;

/// <summary>
/// Entity script context class, contains the user and target alongside game context.
/// </summary>
public sealed class EntityScriptContext : ScriptContext {
    /// <summary>
    /// Constructor for the entity script context class.
    /// </summary>
    /// <param name="gameContext">The game's context object.</param>
    /// <param name="user">The user of the entity script.</param>
    /// <param name="target">The target of the entity script</param>
    public EntityScriptContext(GameContext gameContext, Entity user, Entity target) : base(gameContext) {
        this.user = user;
        this.target = target;
    }

    /// <summary>
    /// Returns the stat short names from the stat name registry.
    /// </summary>
    /// <returns>An array of strings for the stat short names.</returns>
    public string[] GetStatShortNames() {
        return gameContext.GetStatNameRegistry().GetStatShortNames();
    }

    /// <summary>
    /// Getter for the entity scripts user.
    /// </summary>
    /// <returns>The user of the entity script.</returns>
    public Entity GetUser() {
        return user;
    }

    /// <summary>
    /// Getter for the entity scripts target.
    /// </summary>
    /// <returns>The target of the entity script.</returns>
    public Entity GetTarget() {
        return target;
    }

    private readonly Entity user, target;
}