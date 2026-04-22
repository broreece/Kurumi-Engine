using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Repositories.Base;

/// <summary>
/// Used to convert row data to the corect formats.
/// </summary>
public readonly struct ReaderRow 
{
    private readonly SqliteDataReader _reader;

    public ReaderRow(SqliteDataReader reader) 
    {
        _reader = reader;
    }

    /// <summary>
    /// Converts a column to an integer value.
    /// </summary>
    /// <param name="col">The column ID.</param>
    /// <returns>The integer value stored at the column.</returns>
    public int GetInt(int col) => _reader.GetInt32(col);

    /// <summary>
    /// Converts a column to an string value.
    /// </summary>
    /// <param name="col">The column ID.</param>
    /// <returns>The string value stored at the column.</returns>
    public string GetString(int col) => _reader.GetString(col);

    /// <summary>
    /// Converts a column to a nullable string value.
    /// </summary>
    /// <param name="col">The column ID.</param>
    /// <returns>The nullable string value stored at the column.</returns>
    public string? GetNullableString(int col) => _reader.IsDBNull(col) ? null : _reader.GetString(col);

    /// <summary>
    /// Converts a column to an boolean value. We store booleans in a database by 0 = false, 1 = true.
    /// </summary>
    /// <param name="col">The column ID.</param>
    /// <returns>The boolean value stored at the column.</returns>
    public bool GetBool(int col) => _reader.GetInt32(col) == 1;
}