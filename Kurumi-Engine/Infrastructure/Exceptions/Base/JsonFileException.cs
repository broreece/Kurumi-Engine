namespace Infrastructure.Exceptions.Base;

/// <summary>
/// Custom exception class thrown if no json file is found or json format is incorrect.
/// </summary>
public sealed class JsonFileException : EngineException {
    public JsonFileException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Fatal;
}