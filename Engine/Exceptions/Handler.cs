namespace Engine.Exceptions;

using Engine.Logger;
using Engine.Runtime;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

/// <summary>
/// Handler static class for handling errors by logging them and correcting if possible.
/// </summary>
public static class Handler {
    /// <summary>
    /// Handle config exceptions. In all cases these will be fatal and will result in a game crash.
    /// </summary>
    /// <param name="exception">The config exception thrown.</param>
    public static void HandleConfigException(Exception exception) {
        Logger.LogFatal(exception.Message);
        DisplayErrorMessage(exception.Message);
    }

    /// <summary>
    /// Handler for game exceptions, these are exceptions that occur during the game after config is loaded.
    /// </summary>
    /// <param name="exception">The game exception thrown.</param>
    /// <param name="gameContext">The game context.</param>
    public static void HandleGameException(Exception exception, GameContext gameContext) {
        if (exception is EngineException engineException) {
            string message = $@"[{engineException.GetSeverity()}]
                Scene: {gameContext.GetSceneType()}
                Script: {gameContext.GetCurrentScriptName()}
                UI Depth: {gameContext.GetUIStackDepth()}
                Message: {exception.Message}";
            switch (engineException.GetSeverity()) {
                case Severity.Fatal:
                    // TODO: (LI-01) Close game window here.
                    // TODO: (LI-01) Based on logging config output to console.
                    // TODO: (LI-01) Close game window here.
                    Logger.LogFatal(message);
                    DisplayErrorMessage(message);
                    break;

                case Severity.Error:
                    // TODO: (LI-01) Based on logging config output to console.
                    // TODO: (LI-01) Based on logging open a window here.
                    Logger.LogError(message);
                    break;

                case Severity.Warning:
                    // TODO: (LI-01) Based on logging config output to console.
                    // TODO: (LI-01) Based on logging open a window here.
                    Logger.LogWarning(message);
                    break;

                case Severity.Info:
                    // TODO: (LI-01) Based on logging config output to console.
                    // TODO: (LI-01) Based on logging open a window here.
                    Logger.LogInfo(message);
                    break;

                default:
                    break;
            }
        }
        else {
            Logger.LogFatal(exception.Message);
            DisplayErrorMessage(exception.Message);
            // TODO: (LI-01) Close game window here.
        }
    }

    /// <summary>
    /// Function that displays an error message for the user.
    /// </summary>
    /// <param name="message">The message text to be displayed.</param>
    private static void DisplayErrorMessage(string message) {
        var window = new RenderWindow(new VideoMode(500, 250), "Error");

        // TODO: (EMI-01) Change this to config.
        Font font = new("Assets/Fonts/template.otf");

        // TODO: (EMI-01) Change this to config.
        Text text = new(message, font, 18)
        {
            FillColor = Color.Black,
            Position = new Vector2f(20, 60)
        };

        while (window.IsOpen) {
            window.DispatchEvents();
            window.Closed += (sender, args) => window.Close();

            // TODO: (EMI-01) Change this to config.
            window.Clear(new Color(237, 237, 237));
            window.Draw(text);
            window.Display();
        }
    }
}