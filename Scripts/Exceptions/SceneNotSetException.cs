namespace Scripts.Exceptions;

using Engine.Exceptions;

/// <summary>
/// Custom exception class thrown if a scene is not set when loading the scene script context.
/// </summary>
public sealed class SceneNotSetException : EngineException {
    /// <summary>
    /// The constructor for the scene not set exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public SceneNotSetException(string message)
        : base(message, Severity.Error) {
    }
}