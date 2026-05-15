using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Capabilities.Exceptions;

public sealed class MissingGameFlagException : EngineException 
{
    public MissingGameFlagException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}