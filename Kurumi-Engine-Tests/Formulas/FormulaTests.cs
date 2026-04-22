namespace Tests.Formulas;

using Engine.Systems.Combat.Core;
using Engine.Systems.Combat.Factories;
using Xunit;

public class FormulaTests 
{
    [Fact]
    public void FormulaCalculationTest() 
    {
        var statShortNameIndex = new Dictionary<string, int>
        {
            ["atk"] = 0,
            ["def"] = 1
        };

        var damageCalculatorFactory = new DamageCalculatorFactory(statShortNameIndex);
        var damageCalculator = damageCalculatorFactory.Create();

        var test1 = "a.atk - b.def";
        var test2 = "10 - 5";
        var test3 = "a.atk^2";
        var test4 = "b.atk * 2";
        var test5 = "b.def / 2";
        var test6 = "(a.atk * 2) - (b.def * 2)";

        int[] userStats = [ 10, 10 ];
        int[] targetStats = [ 5, 5 ];

        var result1 = damageCalculator.Evaluate(test1, userStats, targetStats);
        var result2 = damageCalculator.Evaluate(test2, userStats, targetStats);
        var result3 = damageCalculator.Evaluate(test3, userStats, targetStats);
        var result4 = damageCalculator.Evaluate(test4, userStats, targetStats);
        var result5 = damageCalculator.Evaluate(test5, userStats, targetStats);
        var result6 = damageCalculator.Evaluate(test6, userStats, targetStats);

        Assert.Equal(5, result1);
        Assert.Equal(5, result2);
        Assert.Equal(100, result3);
        Assert.Equal(10, result4);
        Assert.Equal(2, result5);
        Assert.Equal(10, result6);
    }
}
