// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Context.Capabilities.Exceptions;

public sealed class MissingCapabilityException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public MissingCapabilityException(string message) : base(message) {}
}