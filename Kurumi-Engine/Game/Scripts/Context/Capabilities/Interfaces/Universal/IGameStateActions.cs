using Game.Scripts.Context.Capabilities.Base;

namespace Game.Scripts.Context.Capabilities.Interfaces.Universal;

public interface IGameStateActions : ICapability 
{
    public void ChangeFlag(int flagIndex, bool newValue);

    public bool GetGameFlag(int flagIndex);
}