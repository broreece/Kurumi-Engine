using Infrastructure.Exceptions.Base;

namespace Engine.Assets.Exceptions;

/// <summary>
/// Custom exception class thrown if various errors occur during damage calculations.
/// </summary>
public sealed class DamageCalculatorException : EngineException 
{
    public DamageCalculatorException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}