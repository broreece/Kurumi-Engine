using Data.Definitions.Formations.Core;
using Data.Models.Formations;

namespace Data.Runtime.Formations.Core;

public sealed class Formation 
{
    private readonly FormationDefinition _definition;
    private readonly FormationModel _model;

    internal Formation(FormationDefinition definition, FormationModel model) 
    {
        _definition = definition;
        _model = model;
    }
}
