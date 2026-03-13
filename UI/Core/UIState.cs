namespace UI.Core;

using Engine.Input.Core;
using UI.Component.Core;
using UI.Exceptions;

/// <summary>
/// The public abstract UI state class.
/// </summary>
public abstract class UIState {
    /// <summary>
    /// Constructor for the UI state, initalizers the components list.
    /// </summary>
    protected UIState() {
        closed = false;
        components = new Stack<IUIComponent>();
    }

    /// <summary>
    /// Update function, can be over written if a UI state changes.
    /// </summary>
    /// <param name="paused">If the game is currently paused.</param>
    public virtual void Update(bool paused) {}

    /// <summary>
    /// Getter for the list of components that make up the UI state.
    /// </summary>
    /// <returns>A list of components.</returns>
    public Stack<IUIComponent> GetComponents() {
        return components;
    }

    /// <summary>
    /// Function used to check if a UI state has finished and can be removed from the UI stack.
    /// </summary>
    /// <returns>If the UI state has finished executing.</returns>
    public bool IsClosed() {
        return closed;
    }

    /// <summary>
    /// Boolean used to check if the UI state changes the input mapping.
    /// </summary>
    /// <returns>If the UI state has an input mapping to take precedent.</returns>
    public virtual bool TakesControl() {
        return true;
    }

    /// <summary>
    /// Function used to load the UI states input map.
    /// </summary>
    /// <returns>The input map of the UI state.</returns>
    /// <exception cref="InputMapNotSetException">Error thrown if no input map is set in the UI state when trying to retrieve it.</exception>
    public InputMap GetInputMap() {
        if (inputMap == null) {
            throw new InputMapNotSetException("Input map is not currently assigned.");
        }
        return inputMap;
    }

    /// <summary>
    /// Function that closes the UI state.
    /// </summary>
    protected abstract void Close();

    protected bool closed;
    protected readonly Stack<IUIComponent> components;
    protected InputMap? inputMap;
}