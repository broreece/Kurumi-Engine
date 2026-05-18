using Infrastructure.Exceptions.Base;

namespace Data.Runtime.Maps.Exceptions;

public sealed class ActorNotFoundException : EngineException 
{
    public ActorNotFoundException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}