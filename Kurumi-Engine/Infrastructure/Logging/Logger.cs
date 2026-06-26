// Config.
using Config.Runtime.External;

namespace Infrastructure.Loging;

/// <summary>
/// Logs errors and information into external files or output console depending on logging config.
/// </summary>
public sealed class Logger 
{
    private readonly string _logFile;

    private readonly bool _consoleOutput;

    public Logger(LoggerConfig loggerConfig) 
    {
        var logPath = Path.Combine(
            AppContext.BaseDirectory,
            loggerConfig.LogDirectory
        );
        Directory.CreateDirectory(logPath);

        _logFile = Path.Combine(
            logPath,
            loggerConfig.LogFileName
        );

        _consoleOutput = loggerConfig.ConsoleOutput;
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

        if (_consoleOutput) {
            Console.WriteLine(entry);
        }

        var logFilePath = Path.Combine(
            AppContext.BaseDirectory,
            _logFile
        );

        try 
        {
            File.AppendAllText(logFilePath, entry + Environment.NewLine);
        }
        catch (Exception) 
        {
            if (_consoleOutput) {
                Console.WriteLine($"Could not successfully log message to: {logFilePath}");
            }
        }
    }
}