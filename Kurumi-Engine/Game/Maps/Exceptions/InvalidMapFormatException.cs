using Infrastructure.Exceptions.Base;

namespace Game.Maps.Exceptions;

public sealed class InvalidMapFormatException : EngineException 
{
    public InvalidMapFormatException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}