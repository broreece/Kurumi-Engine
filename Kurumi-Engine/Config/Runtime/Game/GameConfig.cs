namespace Config.Runtime.Game;

public sealed class GameConfig 
{
    public required int MaxPartySize { get; init; }
    public required int MaxEnemyFormationSize { get; init; }
    public required int SaveFiles { get; init; }
    public required int AgilityStatIndex { get; init; }
}