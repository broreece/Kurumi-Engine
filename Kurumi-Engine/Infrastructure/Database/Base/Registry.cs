// Infrastructure.
using Infrastructure.Database.Exceptions;

namespace Infrastructure.Database.Base;

/// <summary>
/// Contains a set of data of a specified type.
/// </summary>
/// <typeparam name="T">The type of data the registry stores.</typeparam>
public sealed class Registry<T> 
{
    private readonly Dictionary<int, T> _data;

    public Registry(IReadOnlyList<T> objects, Func<T, int> idSelector) 
    {
        _data = objects.ToDictionary(idSelector);
    }

    public T Get(int id)
    {
        if (!_data.TryGetValue(id, out var value))
        {
            throw new RegistryIDNotFoundException($"No {typeof(T).Name} found in registry with ID {id}.");
        }

        return value;
    }
}