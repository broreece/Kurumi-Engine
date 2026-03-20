namespace Scripts.EntityScripts.EntityScriptSteps;

using Scripts.Base;
using Scripts.EntityScripts.Base;
using Game.Entities.Base;
using Utils.Strings;

/// <summary>
/// The change HP entity script step.
/// </summary>
public sealed class ChangeHp : ScriptStep {
    /// <summary>
    /// Constructor for the change HP entity script step.
    /// </summary>
    /// <param name="reduceHp">If the change hp script reduces the targets hp.</param>
    /// <param name="canKo">If the change hp script step can KO the target.</param>
    /// <param name="formula">Formula applied to target.</param>
    public ChangeHp(bool reduceHp, bool canKo, string formula) {
        this.reduceHp = reduceHp;
        this.canKo = canKo;
        this.formula = formula;
    }

    /// <summary>
    /// The activation function for the change hp script. Changes the HP of the target applying the formula.
    /// </summary>
    /// <param name="scriptContext">The context of the script.</param>
    public override void Activate(ScriptContext scriptContext) {
        EntityScriptContext entityScriptContext = (EntityScriptContext) scriptContext;
        
        // Reduce duplicate target calls.
        Entity target = entityScriptContext.GetTarget();

        int result = Formula.GetEntityScriptResult(formula, entityScriptContext.GetUser(), target, 
            entityScriptContext.GetStatShortNames());
        int newHp;
        if (reduceHp) {
            if (!canKo && (target.GetCurrentHp() - result) < 0) {
                newHp = 1;
            }
            else if (canKo && (target.GetCurrentHp() - result) <= 0) {
                target.AddStatus(entityScriptContext.GetStatus(0));
                newHp = 0;
            }
            else {
                newHp = target.GetCurrentHp() - result;
            }
        }
        else {
            newHp = target.GetCurrentHp() + result;
        }
        target.SetCurrentHP(newHp);
    }

    /// <summary>
    /// Checks if the script step reduces the hp of the target.
    /// </summary>
    /// <returns>True: If target loses HP.
    /// False: if target gains HP.</returns>
    public bool ReducesHP() {
        return reduceHp;
    }

    /// <summary>
    /// Checks if the script step can KO.
    /// </summary>
    /// <returns>If the script step can induce a KO state.</returns>
    public bool CanKo() {
        return canKo;
    }
    
    /// <summary>
    /// Getter for the script steps formular.
    /// </summary>
    /// <returns>The formula used to determine the amount of HP to change from the target.</returns>
    public string GetFormula() {
        return formula;
    }

    private readonly bool reduceHp, canKo;
    private readonly string formula;
}
