using Infrastructure.Exceptions.Base;

namespace Engine.State.States.Battle.Exceptions;

public sealed class DisplayConfigNotSetException : EngineException 
{
    public DisplayConfigNotSetException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Fatal;
}