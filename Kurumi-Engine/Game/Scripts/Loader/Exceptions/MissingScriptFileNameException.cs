using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Loader.Exceptions;

public sealed class MissingScriptFileNameException : EngineException 
{
    public MissingScriptFileNameException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}