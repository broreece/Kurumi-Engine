// Data.
using Data.Models.Characters.Collections;
using Data.Models.Formations.Collections;
using Data.Models.Inventory;
using Data.Models.Maps.Collections;
using Data.Models.Party;
using Data.Models.Variables;

namespace Infrastructure.Persistance.Base;

/// <summary>
/// Contains game states that can change during game play.
/// </summary>
public sealed class SaveData 
{
    public required PartyModel Party { get; set; }

    public required CharacterModelCollection CharacterCollection { get; set; }
    public required FormationModelCollection FormationCollection { get; set; }

    public required Inventory Inventory { get; set; }
    
    public required GameVariables GameVariables { get; set; }

    public required ActorModelCollection ActorCollection { get; set; }
}