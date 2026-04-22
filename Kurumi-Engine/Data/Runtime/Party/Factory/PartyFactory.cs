namespace Data.Runtime.Party.Factory;

using Data.Models.Party;
using Data.Runtime.Party.Core;

public sealed class PartyFactory 
{
    public Party Create(PartyModel partyModel, Dictionary<int, int> inventory) 
    {
        return new Party{PartyModel = partyModel, Inventory = inventory};
    }
}