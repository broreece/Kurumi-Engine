namespace Scenes.Battle.Exceptions;

/// <summary>
/// Custom exception class thrown if the battle targeting view is not set in the battle scene.
/// </summary>
public sealed class UnsetBattleTargetingViewException : Exception {
    /// <summary>
    /// The constructor for the unset battle targeting view exception.
    /// </summary>
    public UnsetBattleTargetingViewException() {
    }

    /// <summary>
    /// The constructor for the unset battle targeting view exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public UnsetBattleTargetingViewException(string message)
        : base(message) {
    }
}