// Data.
using Data.Definitions.Formations.Core;

namespace Data.Definitions.Formations.Factories;

public sealed class FormationDefinitionFactory 
{
    public FormationDefinition Create(
        int id, 
        string mapName, 
        int returnX, 
        int returnY, 
        int searchTimer, 
        int itemPoolId, 
        int onFoundActorInfoId, 
        int defaultActorInfoId, 
        string backgroundMusicName, 
        string backgroundArtName, 
        string? onLoseScript, 
        string? onWinScript, 
        IReadOnlyList<int> enemies
    ) 
    {
        return new FormationDefinition()
        {
            Id = id, 
            MapName = mapName, 
            ReturnX = returnX, 
            ReturnY = returnY, 
            SearchTimer = searchTimer, 
            ItemPoolId = itemPoolId, 
            OnFoundActorInfoId = onFoundActorInfoId, 
            DefaultActorInfoId = defaultActorInfoId, 
            BackgroundMusicName = backgroundMusicName, 
            BackgroundArtName = backgroundArtName, 
            OnLoseScript = onLoseScript, 
            OnWinScript = onWinScript, 
            Enemies = enemies
        };
    }
}