// Infrastructure.
using Infrastructure.Persistance.Services;

// External libraries.
using Xunit;

namespace Tests.Persistance;

public sealed class PersistanceTests 
{
    [Fact]
    public void NewSaveDataTest()
    {
        var saveService = new SaveService();
        saveService.LoadNewSaveData();
    }

    [Fact]
    public void PersistanceTest()
    {
        var saveService = new SaveService();
        var saveData = saveService.LoadNewSaveData();
        saveService.Save(0, saveData);
        saveService.Load(0);
    }
}