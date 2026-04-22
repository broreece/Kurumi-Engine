namespace Data.Definitions.Actors.Core;

public sealed class ActorInfo 
{
    public required int Id { get; init; }
    public required int SpriteId { get; init; }
    public required int Behaviour { get; init; }
    public required int MovementSpeed { get; init; }
    public required int TrackingRange { get; init; }

    public required string? ScriptName { get; init; }

    public required bool BelowParty { get; init; }
    public required bool Passable { get; init; }
    public required bool OnTouch { get; init; }
    public required bool Auto { get; init; }
    public required bool OnAction { get; init; }
    public required bool OnFind { get; init; }

    public required IReadOnlyList<int> Path { get; init; }
}