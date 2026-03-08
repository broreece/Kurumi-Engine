namespace UI.States.Modal.Modals;

using UI.Interfaces;
using UI.States.Modal.Core;
using SFML.System;

/// <summary>
/// The global message state class, global messages display text without locking controls.
/// </summary>
public sealed class GlobalMessageState : WindowedState {
    /// <summary>
    /// Constructor for the global message state.
    /// </summary>
    /// <param name="windowXPosition">The x position of the global message box.</param>
    /// <param name="windowYPosition">The y position of the global message box.</param> 
    /// <param name="width">The width of the global message box.</param>
    /// <param name="height">The height of the global message box.</param>
    /// <param name="duration">The duration of the global message state.</param>
    /// <param name="windowFileName">The global message window art file name.</param>
    /// <param name="windowFileAccessor">The window file accesor object.</param>
    /// <param name="gameWindowDimensionsAccessor">The game window dimension accessor object.</param>
    /// <param name="textXPosition">The X position of the text component.</param>
    /// <param name="textYPosition">The Y position of the text component.</param>
    /// <param name="fontSize">The font size.</param>
    /// <param name="fontFileName">The font file name.</param>
    /// <param name="text">The paged text the global message displays.</param>
    public GlobalMessageState(int windowXPosition, int windowYPosition, int width, int height, int duration, string windowFileName, 
        IWindowFileAccessor windowFileAccessor, IGameWindowDimensionsAccessor gameWindowDimensionsAccessor, int textXPosition, 
        int textYPosition, float fontSize, string fontFileName, string[,] text) : base(windowXPosition, windowYPosition, width, height, 
        windowFileName, windowFileAccessor, gameWindowDimensionsAccessor, textXPosition, textYPosition, fontSize, fontFileName, text) {
        // Assign default values.
        this.duration = duration;
        clock = new Clock();
        elapsedTime = 0;
    }

    /// <summary>
    /// The global message update function, updates the elapsed time since the previous update.
    /// </summary>
    /// <param name="paused">If the game is currently paused.</param>
    public override void Update(bool paused) {
        if (!paused) {
            elapsedTime += clock.ElapsedTime.AsMilliseconds();
        }
        clock.Restart();
        if (elapsedTime > duration) {
            Close();
        }
    }

    /// <summary>
    /// Global message states control boolean.
    /// </summary>
    /// <returns></returns>
    public override bool TakesControl() {
        return false;
    }

    protected override void Close() {
        // TODO: Implement closing animation.
        closed = true;
    }
    
    // The clock and related fields for the global message window.
    private readonly Clock clock;
    private readonly int duration;
    private int elapsedTime;
}