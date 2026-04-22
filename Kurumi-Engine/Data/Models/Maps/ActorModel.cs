namespace Data.Models.Maps;

public sealed class ActorModel 
{
    public required int XLocation { get; set; }
    public required int YLocation { get; set; }
    public required int ActorID { get; set; } 
    public required int Facing { get; set; }
    
    public required bool Visible { get; set; }
}