namespace Infrastructure.Loging;

/// <summary>
/// Logs errors and information into external files or output console depending on logging config.
/// </summary>
public sealed class Logger 
{
    // TODO: (LI-01) Move this to config.
    private static readonly string logDirectory = "logs";
    private static readonly string logFile = "logs/log.txt";

    public Logger() 
    {
        var logPath = Path.Combine(
            AppContext.BaseDirectory,
            logDirectory
        );
        Directory.CreateDirectory(logPath);
    }

    public void LogInfo(string message) => Write("INFO", message);

    public void LogWarning(string message) => Write("WARNING", message);

    public void LogError(string message) => Write("ERROR", message);

    public void LogFatal(string message) => Write("FATAL", message);

    /// <summary>
    /// Helper function used to write to a file and console depending on config.
    /// </summary>
    /// <param name="level">The level of severity of the message.</param>
    /// <param name="message">The message to write.</param>
    private void Write(string level, string message) 
    {
        var entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

        // TODO: (LI-01) Check that the logger config has the console output enabled.
        Console.WriteLine(entry);

        try 
        {
            var logFilePath = Path.Combine(
                AppContext.BaseDirectory,
                logFile
            );
            File.AppendAllText(logFilePath, entry + Environment.NewLine);
        }
        catch (Exception) 
        {
            // TODO: (LI-01) If console output enabled output that file could not be logged.
        }
    }
}