using Game.Scripts.Context.Capabilities.Exceptions;

namespace Game.Scripts.Context.Capabilities.Base;

/// <summary>
/// Used to store a list of capability objects by type.
/// </summary>
public sealed class CapabilityContainer 
{
    /// <summary>
    /// Returns the capability stored at the object type key. Must be a ICapability object.
    /// </summary>
    /// <typeparam name="T">The type of capability requested.</typeparam>
    /// <returns>The capability of type T.</returns>
    /// <exception cref="MissingCapabilityException">Exception thrown if a capability is not found.</exception>
    public T GetCapability<T>() where T : class, ICapability 
    {
        return _capabilities.TryGetValue(typeof(T), out var capability) ? 
            (T) capability : 
        throw new MissingCapabilityException($"The capability {typeof(T)} was not found in the capability dictionary.");
    }

    /// <summary>
    /// Assigns a new capability to the capabilities containers utilizing the type of capability and the object.
    /// </summary>
    /// <param name="key">The type being stored.</param>
    /// <param name="value">The ICapability object.</param>
    public void SetCapability(Type key, ICapability value) => _capabilities[key] = value;

    private readonly Dictionary<Type, ICapability> _capabilities = [];
}