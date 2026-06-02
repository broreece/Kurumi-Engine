// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Engine.Assets.Exceptions;

/// <summary>
/// Custom exception class thrown if a provided asset type is not found.
/// </summary>
public sealed class AssetTypeInvalidException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public AssetTypeInvalidException(string message) : base(message) {}
}