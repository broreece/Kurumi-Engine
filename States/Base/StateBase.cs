namespace States.Base;

using Engine.Runtime.Core;
using Game.Party;

/// <summary>
/// The state interface class, used for states that update.
/// </summary>
public abstract class StateBase {
    /// <summary>
    /// Constructor for the state base class.
    /// </summary>
    /// <param name="gameContext">The games context.</param>
    protected StateBase(GameContext gameContext) {
        this.gameContext = gameContext;

        // Store party to avoid frequent method calls.
        party = gameContext.GetParty();
    }

    /// <summary>
    /// The update function used to update the game state.
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// Getter for the states party value. Used when Scenes require party information.
    /// </summary>
    /// <returns>The party the state uses.</returns>
    public GameContext GetGameContext() {
        return gameContext;
    }

    protected readonly GameContext gameContext;
    protected readonly Party party;
}
