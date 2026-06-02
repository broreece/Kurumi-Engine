using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Loader.Exceptions;

public sealed class InvalidScriptFormatException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public InvalidScriptFormatException(string message) : base(message) {}
}