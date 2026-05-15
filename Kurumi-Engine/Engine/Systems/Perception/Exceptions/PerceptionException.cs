using Infrastructure.Exceptions.Base;

namespace Engine.Systems.Perception.Exceptions;

public sealed class PerceptionException : EngineException 
{
    public PerceptionException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Warning;
}