// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Data.Runtime.Maps.Exceptions;

public sealed class ActorNotFoundException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public ActorNotFoundException(string message) : base(message) {}
}