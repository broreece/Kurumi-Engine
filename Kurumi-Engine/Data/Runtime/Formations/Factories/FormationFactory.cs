using Data.Definitions.Formations.Core;
using Data.Models.Formations;
using Data.Runtime.Formations.Core;

namespace Data.Runtime.Formations.Factories;

public sealed class FormationFactory 
{
    public Formation Create(FormationDefinition definition, FormationModel model) 
    {
        return new Formation(definition, model);
    }
}