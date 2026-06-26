// Infrastructure.
using Infrastructure.Exceptions.Base;

using Infrastructure.Loging;

// External Libraries.
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Infrastructure.Exceptions.Handler;

/// <summary>
/// Handles errors by logging them and correcting if possible.
/// </summary>
public sealed class Handler 
{
    private readonly Logger _logger;

    public Handler(Logger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handler for game exceptions, these are exceptions that occur during the game after config is loaded.
    /// </summary>
    public void HandleException(Exception exception) 
    {
        if (exception is EngineException engineException) 
        {
            var message = $@"[{engineException.Severity}] {exception.StackTrace}";
            switch (engineException.Severity) 
            {
                case ExceptionSeverity.Fatal:
                    _logger.LogFatal(message);
                    DisplayErrorMessage(message);
                    // TODO: (EMI-01) Close game window here.
                    break;

                case ExceptionSeverity.Error:
                    _logger.LogError(message);
                    DisplayErrorMessage(message);
                    break;

                case ExceptionSeverity.Warning:
                    _logger.LogWarning(message);
                    break;

                case ExceptionSeverity.Info:
                    _logger.LogInfo(message);
                    break;

                default:
                    break;
            }
        }
        
        else 
        {
            var message = exception.Message;
            var stackMessage = $@"Stack: {exception.StackTrace}";
            _logger.LogFatal($"{message} {stackMessage}");
            DisplayErrorMessage(message);
            // TODO: (EMI-01) Close game window here.
        }
    }

    /// <summary>
    /// Function that displays an error message for the user.
    /// </summary>
    /// <param name="message">The message text to be displayed.</param>
    private void DisplayErrorMessage(string message) 
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