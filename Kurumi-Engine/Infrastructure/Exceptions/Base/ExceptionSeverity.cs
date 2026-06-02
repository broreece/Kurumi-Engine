namespace Infrastructure.Exceptions.Base;

/// <summary>
/// Enum representing the severity of an exception, used by the handler and logger.
/// </summary>
public enum ExceptionSeverity 
{
    Fatal,
    Error,
    Warning,
    Info
}