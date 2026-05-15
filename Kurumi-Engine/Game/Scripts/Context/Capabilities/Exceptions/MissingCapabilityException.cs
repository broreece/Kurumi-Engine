using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Capabilities.Exceptions;

public sealed class MissingCapabilityException : EngineException 
{
    public MissingCapabilityException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}