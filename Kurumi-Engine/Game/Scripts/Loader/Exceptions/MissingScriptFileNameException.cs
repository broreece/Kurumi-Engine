using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Loader.Exceptions;

public sealed class MissingScriptFileNameException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public MissingScriptFileNameException(string message) : base(message) {}
}