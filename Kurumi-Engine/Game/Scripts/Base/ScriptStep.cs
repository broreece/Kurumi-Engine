using Game.Scripts.Context.Core;

namespace Game.Scripts.Base;

public abstract class ScriptStep {
    public required string? NextStep { protected get; init; }

    public abstract void Activate(ScriptContext scriptContext);

    public virtual string? GetNextStep() => NextStep;

    public virtual bool Waiting() => false;
}