namespace Config.Runtime.External;

public sealed class LoggerConfig 
{
    public required string LogDirectory { get; init; }
    public required string LogFileName { get; init; }

    public required bool ConsoleOutput { get; init; }
}