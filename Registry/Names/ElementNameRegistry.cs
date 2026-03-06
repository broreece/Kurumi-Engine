namespace Registry.Names;

/// <summary>
/// The element name data registry, contains data about the element names.
/// </summary>
public sealed class ElementNameRegistry {
    /// <summary>
    /// Constructor for the element name data registry.
    /// </summary>
    /// <param name="elementNames">The element names.</param>
    public ElementNameRegistry(string[] elementNames) {
        this.elementNames = elementNames;
    }

    /// <summary>
    /// Getter for a specific element name.
    /// </summary>
    /// <param name="index">The index of the desired element name.</param>
    /// <returns>The element name.</returns>
    public string GetElementName(int index) {
        return elementNames[index];
    }

    private readonly string[] elementNames;
}