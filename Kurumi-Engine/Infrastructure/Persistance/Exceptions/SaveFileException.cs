// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Infrastructure.Persistance.Exceptions;

/// <summary>
/// Thrown if any exception occurs when a save file is being loaded.
/// </summary>
public sealed class SaveFileException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Fatal;

    public SaveFileException(string message) : base(message) {}
}