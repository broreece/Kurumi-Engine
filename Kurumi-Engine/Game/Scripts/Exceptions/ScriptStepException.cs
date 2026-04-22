using Infrastructure.Exceptions.Base;

namespace Game.Scripts.Exceptions;

/// <summary>
/// Occurs when loading or creating script steps.
/// </summary>
public sealed class ScriptStepException : EngineException 
{
    public ScriptStepException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}