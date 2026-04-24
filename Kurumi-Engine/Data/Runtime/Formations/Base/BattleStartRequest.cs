namespace Data.Runtime.Formations.Base;

/// <summary>
/// Used to store information regarding a battle start request. Stored in game objects.
/// </summary>
public sealed class BattleStartRequest 
{
    public required string BackgroundMusicName { get; init; }
    public required string BattleBackgroundArtName { get; init; }
    public required int EnemyFormationId { get; init; }
}