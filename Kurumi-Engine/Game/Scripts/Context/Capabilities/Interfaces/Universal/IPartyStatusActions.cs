using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Universal;

public interface IPartyStatusActions : ICapability 
{
    public void AddStatusToParty(int statusId);
}