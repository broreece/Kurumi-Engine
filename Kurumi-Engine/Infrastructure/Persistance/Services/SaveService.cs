using System.Text.Json;

using Infrastructure.Persistance.Base;
using Infrastructure.Persistance.Exceptions;

namespace Infrastructure.Persistance.Services;

public sealed class SaveService 
{
    private readonly string _saveRootPath;
    
    private readonly JsonSerializerOptions _options;

    public SaveService(string savePath) 
    {
        _options = new JsonSerializerOptions() { WriteIndented = true };
        _saveRootPath = Path.Combine(
            AppContext.BaseDirectory,
            "Saves"
        );
    }

    public void Save(int saveFile, SaveData saveData) 
    {
        var saveDirectory = Path.Combine(
            _saveRootPath,
            $"save_{saveFile}"
        );

        // Ensure directory exists
        if (!Directory.Exists(_saveRootPath)) 
        {
            Directory.CreateDirectory(_saveRootPath);
        }
        
        // Save the file.
        var json = JsonSerializer.Serialize(saveData, _options);
        File.WriteAllText(saveDirectory, json);
    }

    /// <summary>
    /// Loads the selected slot's data into the save data.
    /// </summary>
    /// <param name="saveFile">The active save file slot.</param>
    /// <returns>The save data found at the save file.</returns>
    /// <exception cref="SaveFileException">Error thrown if save file was not found or corrupted.</exception>
    public SaveData Load(int saveFile) 
    {
        var saveDirectory = Path.Combine(
            _saveRootPath,
            $"save_{saveFile}"
        );
        
        if (!File.Exists(saveDirectory)) 
        {
            throw new SaveFileException($"Save file at: {saveFile} was not found.");
        }

        var json = File.ReadAllText(saveDirectory);
        return JsonSerializer.Deserialize<SaveData>(json, _options)
               ?? throw new SaveFileException($"Save file at: {saveFile} has been corrupted.");
    }

    /// <summary>
    /// Loads a new save data using the new game data file.
    /// </summary>
    /// <returns>The save data of a new game file.</returns>
    public SaveData LoadNewSaveData() 
    {
        var saveDirectory = Path.Combine(
            _saveRootPath,
            "new_game_data.json"
        );
        
        if (!File.Exists(saveDirectory)) 
        {
            throw new SaveFileException($"No new game data found.");
        }

        var json = File.ReadAllText(saveDirectory);
        return JsonSerializer.Deserialize<SaveData>(json, _options)
               ?? throw new SaveFileException($"New game data has been corrupted");
    }
}