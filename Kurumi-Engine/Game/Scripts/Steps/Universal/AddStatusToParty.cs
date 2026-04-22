using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;

namespace Game.Scripts.Steps.Universal;

public sealed class AddStatusToParty : ScriptStep 
{
    private readonly int statusId;

    public AddStatusToParty(int statusId) : base() 
    {
        this.statusId = statusId;
    }

    public override void Activate(ScriptContext scriptContext) 
    {
        IPartyStatusActions partyStatusActions = scriptContext.GetCapability<IPartyStatusActions>();
        partyStatusActions.AddStatusToParty(statusId);
    }
}
