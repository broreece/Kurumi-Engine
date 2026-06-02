// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Engine.State.States.Battle.Exceptions;

public sealed class DisplayConfigNotSetException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Fatal;

    public DisplayConfigNotSetException(string message) : base(message) {}
}