// Game.
using Game.Scripts.Base;
using Game.Scripts.Context.Capabilities.Interfaces.Universal;
using Game.Scripts.Context.Core;
using Game.Scripts.Steps.Base;

namespace Game.Scripts.Steps.Universal;

public sealed class CheckItemInInventory : ConditionalScriptStep 
{
    private readonly int _itemId;
    private readonly int _amount;
    private readonly IntegerComparison _comparisonOperator;

    private bool _conditionMet;

    public CheckItemInInventory(int itemId, int amount, int comparisonOperator, string? nextIfFalse) : base() 
    {
        _itemId = itemId;
        _amount = amount;
        _comparisonOperator = (IntegerComparison) comparisonOperator;
        NextIfFalse = nextIfFalse;
    }
    
    public override void Activate(ScriptContext scriptContext) 
    {
        IItemActions itemActions = scriptContext.GetCapability<IItemActions>();
        _conditionMet = false;
        switch (_comparisonOperator) {
            case IntegerComparison.GreaterThan:
                _conditionMet = itemActions.ContainsMoreThenOfItem(_itemId, _amount);
                break;

            case IntegerComparison.EqualTo:
                _conditionMet = itemActions.ContainsSameAmountOfItem(_itemId, _amount);
                break;

            case IntegerComparison.LessThan:
                _conditionMet = itemActions.ContainsLessThenOfItem(_itemId, _amount);
                break;

            case IntegerComparison.NotEqualTo:
                _conditionMet = itemActions.ContainsDifferentAmountOfItem(_itemId, _amount);
                break;

            default:
                break;
        }
    }

    protected override bool IsConditionMet() => _conditionMet;
}