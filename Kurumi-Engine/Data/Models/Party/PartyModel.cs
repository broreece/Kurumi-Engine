namespace Data.Models.Party;

public sealed class PartyModel 
{
    public required bool Visible { get; set; }

    public required string MapName { get; set; }

    public required int XLocation { get; set; }
    public required int YLocation { get; set; }
    public required int Facing { get; set; }
    
    public required List<int> PartyMembers { get; set; } = [];
}