// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Engine.Systems.Perception.Exceptions;

public sealed class PerceptionException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Warning;

    public PerceptionException(string message) : base(message) {}
}