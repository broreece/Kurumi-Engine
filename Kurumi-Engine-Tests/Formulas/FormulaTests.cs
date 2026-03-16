namespace Tests.Formulas;

using Game.Entities.Enemy;
using Utils.Strings;
using Xunit;

/// <summary>
/// The formula tests class.
/// </summary>
public class FormulaTests {
    /// <summary>
    /// Formula calculation test.
    /// </summary>
    [Fact]
    public void FormulaCalculationTest() {
        string test1 = "10 - 5";
        string test2 = "a.tes - b.tes";
        // Test BIDMAS operations.
        string test3 = "a.tes^2";
        string test4 = "b.tes * 2";
        string test5 = "b.tes / 2";
        string test6 = "(a.tes * 2) - (b.tes * 2)";
        Enemy testEntity1 = new("test 1", "Test description", 10, 0, [10], [0], [0], []);
        Enemy testEntity2 = new("test 2", "Test description", 10, 0, [5], [0], [0], []);
        string[] statShortNames = ["tes"];
        int result1 = Formula.GetEntityScriptResult(test1, testEntity1, testEntity2, statShortNames);
        int result2 = Formula.GetEntityScriptResult(test2, testEntity1, testEntity2, statShortNames);
        int result3 = Formula.GetEntityScriptResult(test3, testEntity1, testEntity2, statShortNames);
        int result4 = Formula.GetEntityScriptResult(test4, testEntity1, testEntity2, statShortNames);
        int result5 = Formula.GetEntityScriptResult(test5, testEntity1, testEntity2, statShortNames);
        int result6 = Formula.GetEntityScriptResult(test6, testEntity1, testEntity2, statShortNames);
        Assert.Equal(5, result1);
        Assert.Equal(5, result2);
        Assert.Equal(100, result3);
        Assert.Equal(10, result4);
        Assert.Equal(2, result5);
        Assert.Equal(10, result6);
    }
}
