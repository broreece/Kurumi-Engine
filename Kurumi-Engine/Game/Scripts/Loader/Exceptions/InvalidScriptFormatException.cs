using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Loader.Exceptions;

public sealed class InvalidScriptFormatException : EngineException 
{
    public InvalidScriptFormatException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}