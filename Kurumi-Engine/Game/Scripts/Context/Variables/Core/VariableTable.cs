using Game.Scripts.Context.Variables.Base;
using Game.Scripts.Context.Variables.Exceptions;

namespace Game.Scripts.Context.Variables.Core;

/// <summary>
/// Contains a dictionary of variables with unique variable keys.
/// </summary>
public class VariableTable 
{
    private readonly Dictionary<string, object> _variables = [];

    /// <summary>
    /// Getter function used to access an element in the variables dictionary.
    /// </summary>
    /// <param name="key">The key being used.</param>
    /// <typeparam name="T">The type of variable desired.</typeparam>
    /// <returns>The T typed object at the given key index.</returns>
    /// <exception cref="MissingCapabilityException">Exception thrown if a capability is not found.</exception>
    public T GetVariable<T>(ScriptVariableKey<T> key) 
    {
        if (!_variables.TryGetValue(key.Name, out var value)) 
        {
            throw new VariableNotFoundException($"Variable '{key}' not found");
        }

        if (value is not T typed) 
        {
            throw new IncorrectTypeVariableException($"Variable '{key}' is not of type {typeof(T).Name}");
        }

        return typed;
    }

    /// <summary>
    /// Function that sets a variable to the variable table object.
    /// </summary>
    /// <param name="key">The variable key.</param>
    /// <param name="value">The T type object being stored.</param>
    /// <typeparam name="T">The type of variable being stored.</typeparam>
    public void SetVariable<T>(ScriptVariableKey<T> key, T value) => _variables[key.Name] = value!;
}