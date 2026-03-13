namespace Engine.Logger;

/// <summary>
/// Logger static class for logging errors and information into external files or output console depending on logging config.
/// </summary>
public static class Logger {
    /// <summary>
    /// Static function that creates the log directory if it does not exist.
    /// </summary>
    // TODO: (LI-01) Remove static logger make it an object. Pass config into constructor to create variables.
    static Logger() {
        Directory.CreateDirectory(logDirectory);
    }

    /// <summary>
    /// Function used to log info.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public static void LogInfo(string message) {
        Write("INFO", message);
    }

    /// <summary>
    /// Function used to log warning.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public static void LogWarning(string message) {
        Write("WARNING", message);
    }

    /// <summary>
    /// Function used to log error.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public static void LogError(string message) {
        Write("ERROR", message);
    }

    /// <summary>
    /// Function used to log fatal.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public static void LogFatal(string message) {
        Write("FATAL", message);
    }

    /// <summary>
    /// Helper function used to write to a file and console if config is set a message.
    /// </summary>
    /// <param name="level">The level of severity of the message.</param>
    /// <param name="message">The message to write.</param>
    private static void Write(string level, string message) {
        string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

        // TODO: (LI-01) Check that the logger config has the console output enabled.
        Console.WriteLine(entry);

        try {
            File.AppendAllText(logFile, entry + Environment.NewLine);
        }
        catch (Exception) {
            // TODO: (LI-01) If console output enabled output that file could not be logged.
        }
    }

    // TODO: (LI-01) Move this to config.
    private static readonly string logDirectory = "logs";
    private static readonly string logFile = "logs/log.txt";
}