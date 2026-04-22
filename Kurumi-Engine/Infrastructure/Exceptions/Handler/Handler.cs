using Infrastructure.Exceptions.Base;
using Infrastructure.Loging;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Infrastructure.Exceptions.Handler;

/// <summary>
/// Handles errors by logging them and correcting if possible.
/// </summary>
public static class Handler 
{
    private static readonly Logger Logger = new();

    /// <summary>
    /// Handle config exceptions. In all cases these will be fatal and will result in a game crash.
    /// </summary>
    /// <param name="exception">The config exception thrown.</param>
    public static void HandleConfigException(Exception exception) 
    {
        var message = $"{exception.Message}, {exception.StackTrace}";
        Logger.LogFatal(message);
        DisplayErrorMessage(exception.Message);
    }

    /// <summary>
    /// Function used to ignore severity of an exception and just log it's information to the logger.
    /// </summary>
    /// <param name="exception">The info exception.</param>
    public static void HandleInfoException(Exception exception) => Logger.LogInfo(exception.Message);

    /// <summary>
    /// Handler for game exceptions, these are exceptions that occur during the game after config is loaded.
    /// </summary>
    /// <param name="exception">The game exception thrown.</param>
    /// <param name="stack">The error's stack trace.</param>
    public static void HandleGameException(Exception exception, string stack) 
    {
        if (exception is EngineException engineException) 
        {
            var message = $@"[{engineException.Severity}]
                {stack}";
            var stackText = $@"
                Stack: {exception.StackTrace}";
            switch (engineException.Severity) 
            {
                case ExceptionSeverity.Fatal:
                    // TODO: (LI-01) Close game window here.
                    // TODO: (LI-01) Based on logging config output to console.
                    // TODO: (LI-01) Close game window here.
                    Logger.LogFatal($"{message}{stackText}");
                    DisplayErrorMessage(message);
                    break;

                case ExceptionSeverity.Error:
                    // TODO: (LI-01) Based on logging config output to console.
                    // TODO: (LI-01) Based on logging open a window here.
                    Logger.LogError($"{message}{stackText}");
                    break;

                case ExceptionSeverity.Warning:
                    // TODO: (LI-01) Based on logging config output to console.
                    // TODO: (LI-01) Based on logging open a window here.
                    Logger.LogWarning($"{message}{stackText}");
                    break;

                case ExceptionSeverity.Info:
                    // TODO: (LI-01) Based on logging config output to console.
                    // TODO: (LI-01) Based on logging open a window here.
                    Logger.LogInfo($"{message}{stackText}");
                    break;

                default:
                    break;
            }
        }
        else {
            var message = $@"{exception.Message}";
            var stackMessage = $@"
                Stack: {exception.StackTrace}";
            Logger.LogFatal($"{message}{stackMessage}");
            DisplayErrorMessage(message);
            // TODO: (LI-01) Close game window here.
        }
    }

    /// <summary>
    /// Function that displays an error message for the user.
    /// </summary>
    /// <param name="message">The message text to be displayed.</param>
    private static void DisplayErrorMessage(string message) 
    {
        var window = new RenderWindow(new VideoMode(500, 250), "Error");

        // TODO: (EMI-01) Change this to config.
        var fontPath = Path.Combine(
            AppContext.BaseDirectory,
            "Assets",
            "Fonts",
            "template.otf"
        );
        var font = new Font(fontPath);

        // TODO: (EMI-01) Change this to config.
        var text = new Text(message, font, 18)
        {
            FillColor = Color.Black,
            Position = new Vector2f(20, 60)
        };

        while (window.IsOpen) 
        {
            window.DispatchEvents();
            window.Closed += (sender, args) => window.Close();

            // TODO: (EMI-01) Change this to config.
            window.Clear(new Color(237, 237, 237));
            window.Draw(text);
            window.Display();
        }
    }
}