using Infrastructure.Exceptions.Base;

namespace Game.Maps.Exceptions;

public sealed class InvalidMapFormatException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public InvalidMapFormatException(string message) : base(message) {}
}