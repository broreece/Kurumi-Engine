namespace Data.Models.Formations;

public sealed class FormationModel 
{
    public required int X { get; set; }
    public required int Y { get; set; }

    public required bool Found { get; set; }
    public required bool Dead { get; set; }
    
    public required List<EnemyModel> Enemies { get; set; } = [];
}