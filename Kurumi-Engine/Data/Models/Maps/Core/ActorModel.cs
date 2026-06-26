namespace Data.Models.Maps.Core;

public sealed class ActorModel 
{
    public required string ActorKey { get; set; }

    public required int ActorID { get; set; } 

    public required int XLocation { get; set; }
    public required int YLocation { get; set; }
    public required int Facing { get; set; }
    
    public required bool Visible { get; set; }
    public required bool Passable { get; set; }
}