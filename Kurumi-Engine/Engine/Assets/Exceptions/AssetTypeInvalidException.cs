using Infrastructure.Exceptions.Base;

namespace Engine.Assets.Exceptions;

/// <summary>
/// Custom exception class thrown if a provided asset type is not found.
/// </summary>
public sealed class AssetTypeInvalidException : EngineException 
{
    public AssetTypeInvalidException(string message) : base(message) {}

    public override ExceptionSeverity Severity => ExceptionSeverity.Error;
}