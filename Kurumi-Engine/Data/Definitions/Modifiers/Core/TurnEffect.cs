using Data.Definitions.Modifiers.Base;

namespace Data.Definitions.Modifiers.Core;

public sealed class TurnEffect : IEntityModifier 
{
    public required string TurnScriptName { get; init; }
}