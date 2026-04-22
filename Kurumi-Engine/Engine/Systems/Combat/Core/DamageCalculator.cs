namespace Engine.Systems.Combat.Core;

/// <summary>
/// System that can calculate values based on a fixed string format.
/// </summary>
public sealed class DamageCalculator 
{
    private readonly IReadOnlyDictionary<string, int> _statShortNameIndex;

    /// <summary>
    /// Takes the stat short name index provided in the database.
    /// </summary>
    /// <param name="statShortNameIndex">The stat short name index.</param>
    internal DamageCalculator(IReadOnlyDictionary<string, int> statShortNameIndex) {
        _statShortNameIndex = statShortNameIndex;
    }

    public int Evaluate(string formula, int[] userStats, int[] targetStats) 
    {
        List<string> tokens = Tokenize(formula);

        Stack<int> values = [];
        Stack<string> operations = [];

        foreach (var token in tokens) 
        {
            // Number.
            if (int.TryParse(token, out int number)) 
            {
                values.Push(number);
            }
            // Variable (a.atk, b.def, etc.).
            else if (token.Contains('.')) 
            {
                values.Push(ResolveVariable(token, userStats, targetStats));
            }
            // Opening bracket.
            else if (token == "(") 
            {
                operations.Push(token);
            }
            // Closing bracket.
            else if (token == ")") 
            {
                while (operations.Peek() != "(") 
                {
                    EvaluateTop(values, operations);
                }
                operations.Pop();
            }
            // Operator.
            else 
            {
                while (operations.Count > 0 && Precedence(operations.Peek()) >= Precedence(token)) 
                {
                    EvaluateTop(values, operations);
                }

                operations.Push(token);
            }
        }

        // Final resolution.
        while (operations.Count > 0) 
        {
            EvaluateTop(values, operations);
        }

        return values.Pop();
    }

    /// <summary>
    /// Function used to generate a list of string tokens representing operations and variables.
    /// </summary>
    /// <param name="formula">The string formula.</param>
    /// <returns>A list of tokens and variables E.G: ["(","a.atk","-","b.def",")"]</returns>
    private List<string> Tokenize(string formula) 
    {
        List<string> tokens = [];
        string current = "";

        foreach (var character in formula.Replace(" ", "")) {
            if ("+-*/^()".Contains(character)) {
                // If finished building a variable push it into the token stack.
                if (current.Length > 0) {
                    tokens.Add(current);
                    current = "";
                }
                // After confirming variable is on stack add the operator to token stack.
                tokens.Add(character.ToString());
            }
            // If checking variable characters build it up.
            else {
                current += character;
            }
        }

        // Add edge case operator.
        if (current.Length > 0) {
            tokens.Add(current);
        }

        return tokens;
    }

    /// <summary>
    /// Functions used to resolve a variable stored as an entity stat E.G: "a.atk".
    /// </summary>
    /// <param name="token">The token that contained the '.' seperator.</param>
    /// <param name="userStats">The array of user stats.</param>
    /// <param name="targetStats">The array of target stats.</param>
    /// <returns>The integer stat value of the variable</returns>
    /// TODO: Add exception message here.
    private int ResolveVariable(string token, int[] userStats, int[] targetStats) 
    {
        // Token here has to be '.' as we use it to seperate entity and stat such as 'a.atk'
        string[] parts = token.Split('.');

        if (parts.Length != 2) 
        {
            // TODO: (DKE-01) Custom exception here, the token passed wasn't in our format.
            throw new Exception($"Invalid variable format: {token}");
        }

        var entity = parts[0];
        var statName = parts[1].ToLowerInvariant();

        // TODO: (DKE-01) Custom exception here, stat does not exist.
        if (!_statShortNameIndex.TryGetValue(statName, out int statIndex)) 
        {
            throw new Exception($"Unknown stat: {statName}");
        }

        var stats = entity == "a" ? userStats : targetStats;

        return stats[statIndex];
    }

    /// <summary>
    /// Function used to load the left hand, right hand and operation and apply the operation on the two values.
    /// </summary>
    /// <param name="values">The stack of integer values.</param>
    /// <param name="operations">The stack of string operations.</param>
    private void EvaluateTop(Stack<int> values, Stack<string> operations) 
    {
        var secondValue = values.Pop();
        var firstValue = values.Pop();
        var operation = operations.Pop();

        values.Push(Apply(firstValue, secondValue, operation));
    }

    /// <summary>
    /// Function used to apply operation to two values.
    /// </summary>
    /// <param name="firstValue">The first or lefthand value.</param>
    /// <param name="secondValue">The second or righthand value.</param>
    /// <param name="operation">The operation being performed on the values.</param>
    /// <returns>The integer value of the operation performed with the two values.</returns>
    private int Apply(int firstValue, int secondValue, string operation) 
    {
        return operation switch 
        {
            "+" => firstValue + secondValue,
            "-" => firstValue - secondValue,
            "*" => firstValue * secondValue,
            // TODO: (DKE-01) Custom exception here, division by 0.
            "/" => secondValue == 0 ? 0 : firstValue / secondValue,
            "^" => (int) Math.Pow(firstValue, secondValue),
            // TODO: (DKE-01) Custom exception here.
            _ => throw new Exception($"Unknown operator: {operation}")
        };
    }

    /// <summary>
    /// Function that converts an operation to a precedence integer representing BIDMAS.
    /// </summary>
    /// <param name="operation">The operation being checked.</param>
    /// <returns>An integer representing the operation's precendence.</returns>
    private int Precedence(string operation) 
    {
        return operation switch 
        {
            "^" => 3,
            "*" or "/" => 2,
            "+" or "-" => 1,

            _ => 0
        };
    }
}