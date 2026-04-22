using Microsoft.Data.Sqlite;

namespace Infrastructure.Database.Services;

/// <summary>
/// Contains functions that allow querying from the database.
/// </summary>
public sealed class DatabaseService 
{
    private readonly SqliteConnection _connection;

    public DatabaseService(string localPath) 
    {
        _connection = new($"Data Source={localPath}");
    }

    /// <summary>
    /// Function used to query the database and returns a SqliteDataReader result object.
    /// </summary>
    /// <param name="query">The query of the database.</param>
    /// <returns>The SqliteDataReader result of the query.</returns>
    public SqliteDataReader Query(string query) 
    {
        _connection.Open();

        SqliteCommand command = _connection.CreateCommand();
        command.CommandText = query;

        return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
    }
}
