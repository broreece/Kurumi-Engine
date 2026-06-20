// Infrastructure.
using Infrastructure.Database.Exceptions;

namespace Infrastructure.Database.Base;

/// <summary>
/// Contains a lookup dictionary for data tied to a string key.
/// </summary>
/// <typeparam name="T">The type of data the index can be used to lookup.</typeparam>
public sealed class Index<T> 
{
    private readonly IReadOnlyDictionary<string, T> _data;

    public Index(IReadOnlyDictionary<string, T> data) 
    {
        _data = data;
    }

    public T Get(string key)
    {
        if (_data.TryGetValue(key, out var value))
        {
            return value;
        }
        throw new IndexKeyNotFoundException($"No {typeof(T).Name} found in index with key {key}.");
    }
}