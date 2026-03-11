namespace Scripts.Exceptions;

/// <summary>
/// Custom exception class thrown if a scene is not set when loading the scene script context.
/// </summary>
public sealed class SceneNotSetException : Exception {
    /// <summary>
    /// The constructor for the scene not set exception.
    /// </summary>
    public SceneNotSetException() {
    }

    /// <summary>
    /// The constructor for the scene not set exception.
    /// </summary>
    /// <param name="message">A message that can be passed for debugging.</param>
    public SceneNotSetException(string message)
        : base(message) {
    }
}