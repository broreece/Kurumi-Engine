// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Capabilities.Exceptions;

public sealed class MissingGameFlagException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public MissingGameFlagException(string message) : base(message) {}
}