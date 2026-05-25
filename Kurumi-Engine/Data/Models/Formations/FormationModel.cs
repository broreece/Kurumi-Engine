namespace Data.Models.Formations;

public sealed class FormationModel 
{
    public required int XLocation { get; set; }
    public required int YLocation { get; set; }
    public required int Facing { get; set; }

    public required bool Found { get; set; }
    public required bool Dead { get; set; }
    
    public required List<EnemyModel> Enemies { get; set; } = [];
}