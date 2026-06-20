// Data.
using Data.Models.Formations.Core;
using Data.Models.Formations.Exceptions;

namespace Data.Models.Formations.Collections;

public sealed class FormationModelCollection
{
    public required Dictionary<int, FormationModel> Formations { get; set; }

    public FormationModel Get(int formationId)
    {
        if (Formations.TryGetValue(formationId, out var formationModel))
        {
            return formationModel;
        }
        throw new FormationNotFoundException($"No Formation model found with the ID: {formationId}");
    }
}