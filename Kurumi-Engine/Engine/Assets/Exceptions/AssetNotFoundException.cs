// Infrastructure.
using Infrastructure.Exceptions.Base;

namespace Engine.Assets.Exceptions;

/// <summary>
/// Custom exception class thrown if a provided asset is not found.
/// </summary>
public sealed class AssetNotFoundException : EngineException 
{
    public override ExceptionSeverity Severity => ExceptionSeverity.Error;

    public AssetNotFoundException(string message) : base(message) {}
}