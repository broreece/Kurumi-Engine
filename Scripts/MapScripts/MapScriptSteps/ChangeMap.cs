namespace Scripts.MapScripts.MapScriptSteps;

using Scripts.Base;
using Game.Party;

/// <summary>
/// The change map, map scene script step.
/// </summary>
public sealed class ChangeMap : ScriptStep {
    /// <summary>
    /// Change map scene script constructor.
    /// </summary>
    /// <param name="mapId">The map id that the change map script loads.</param>
    /// <param name="xLocation">The x location that the script will set the party to.</param>
    /// <param name="yLocation">The y location that the script will set the party to.</param>
    public ChangeMap(int mapId, int xLocation, int yLocation) {
        this.mapId = mapId;
        this.xLocation = xLocation;
        this.yLocation = yLocation;
    }

    /// <summary>
    /// The activation function for the change map script. Changes the map to a selected map.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        Party party = scriptContext.GetGameContext().GetParty();
        party.SetCurrentMapId(mapId);
        party.SetXLocation(xLocation);
        party.SetYLocation(yLocation);
        scriptContext.GetGameContext().LoadNewMap();
    }

    /// <summary>
    /// Getter for the map's new ID.
    /// </summary>
    /// <returns>The new map id that the even moves the party to.</returns>
    public int GetMapId() {
        return mapId;
    }

    /// <summary>
    /// Getter for the new X location of the party.
    /// </summary>
    /// <returns>The new x location after the script executes.</returns>
    public int GetXLocation() {
        return xLocation;
    }

    /// <summary>
    /// Getter for the new Y location of the party.
    /// </summary>
    /// <returns>The new y location after the script executes.</returns>
    public int GetYLocation() {
        return yLocation;
    }

    private readonly int mapId, xLocation, yLocation;
}