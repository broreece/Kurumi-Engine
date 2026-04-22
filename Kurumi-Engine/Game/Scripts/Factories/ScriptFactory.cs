using Game.Scripts.Core;
using Game.Scripts.Serialization;
using Game.Scripts.Services;

namespace Game.Scripts.Factories;

public sealed class ScriptFactory
{
    private readonly ScriptDataConverter _scriptDataConverter;

    public ScriptFactory(ScriptDataConverter scriptDataConverter)
    {
        _scriptDataConverter = scriptDataConverter;
    }

    public Script Create(ScriptData scriptData)
    {
        return new Script(scriptData, _scriptDataConverter);
    }
}