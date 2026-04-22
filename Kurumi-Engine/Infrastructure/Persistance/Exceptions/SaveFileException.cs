using Infrastructure.Exceptions.Base;

namespace Infrastructure.Persistance.Exceptions;

/// <summary>
/// Thrown if any exception occurs when a save file is being loaded.
/// </summary>
public sealed class SaveFileException : EngineException 
{
    public SaveFileException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Fatal;
}