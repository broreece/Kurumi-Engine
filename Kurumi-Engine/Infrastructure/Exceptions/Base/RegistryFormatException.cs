namespace Infrastructure.Exceptions.Base;

/// <summary>
/// Custom exception class thrown if a provided registry is in the incorrect format.
/// </summary>
public sealed class RegistryFormatException : EngineException 
{
    public RegistryFormatException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Fatal;
}