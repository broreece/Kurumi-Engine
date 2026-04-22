using Data.Definitions.Modifiers.Base;

namespace Data.Definitions.Modifiers.Core;

public sealed class HitChanceModifier : IEntityModifier 
{
    public required int AccuracyModifier { get; init; }
    public required int EvasionModifier { get; init; }
}