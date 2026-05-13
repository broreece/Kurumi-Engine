using Data.Models.Characters;
using Data.Models.Formations;
using Data.Models.Party;
using Data.Models.Variables;

namespace Infrastructure.Persistance.Base;

/// <summary>
/// Contains game states that can change during game play.
/// </summary>
public sealed class SaveData 
{
    public required PartyModel Party { get; set; }

    public required Dictionary<int, CharacterModel> Characters { get; set; }
    public required Dictionary<int, FormationModel> Formations { get; set; }

    public required Dictionary<int, int> Inventory { get; set; }
    
    public required GameVariables GameVariables { get; set; }
}