using Data.Definitions.Formations.Core;

namespace Data.Definitions.Formations.Factories;

public sealed class FormationDefinitionFactory 
{
    public FormationDefinition Create(
        int id, 
        int returnX, 
        int returnY, 
        int searchTimer, 
        int itemPoolId, 
        int onFoundActorInfoId, 
        int defaultActorInfoId, 
        string mapName, 
        string? onLoseScript, 
        string? onWinScript, 
        IReadOnlyList<int> enemies) 
    {
        return new FormationDefinition()
        {
            Id = id, 
            ReturnX = returnX, 
            ReturnY = returnY, 
            SearchTimer = searchTimer, 
            ItemPoolId = itemPoolId, 
            OnFoundActorInfoId = onFoundActorInfoId, 
            DefaultActorInfoId = defaultActorInfoId, 
            MapName = mapName, 
            OnLoseScript = onLoseScript, 
            OnWinScript = onWinScript, 
            Enemies = enemies
        };
    }
}