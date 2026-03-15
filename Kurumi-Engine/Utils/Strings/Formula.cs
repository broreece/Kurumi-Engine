namespace Utils.Strings;

using Game.Entities.Base;

/// <summary>
/// The formula class, this is shared across script steps to perform complex dynamic calculations from strings.
/// The code here is isolated for readbility as it's recursive and large.
/// </summary>
public static class Formula {
    /// <summary>
    /// Returns the result of the executed formula applying with a user and target entity.
    /// </summary>
    /// <param name="formula">The formula to calculate the resulting effect.</param>
    /// <param name="user">The user or 'a' of the formula.</param>
    /// <param name="target">The target of 'b' of the formula.</param>
    /// <param name="statShortNames">The array of stat short names.</param>
    /// <returns>The result of the executed formula.</returns>
    public static int GetEntityScriptResult(string formula, Entity user, Entity target, string[] statShortNames) {
        formula = formula.Replace(" ", string.Empty);
        formula = ReplaceCharacters(formula, user, target, statShortNames);
        formula = Calculate(formula);
        return int.Parse(formula);
    }

    /// <summary>
    /// Private helper function that replaces any entity reference 'a' or 'b'
    /// with the stats of the entity.
    /// </summary>
    /// <param name="formula">Formula including 'a' and 'b' needing to be replaced</param>
    /// <param name="user">The user or 'a' of the formula.</param>
    /// <param name="target">The target of 'b' of the formula.</param>
    /// <param name="statShortNames">The array of stat short names.</param>
    /// <returns>The formula without the a and b targets but with the stats in place.</returns>
    private static string ReplaceCharacters(string formula, Entity user, Entity target, string[] statShortNames) {
        for (int index = 0; index < formula.Length; index ++) {
            char character = formula[index];
            if (character == '.') {
                // Load the short stat name index of the current stat to load the entities stat.
                string stat = formula.Substring(index + 1, 3);
                // Load the entity and then the stat value.
                char entityChar = formula[index - 1];
                Entity entity = entityChar == 'a' ? user : target;
                int statValue = 0;
                // Check for static values stored in stats.
                int statIndex = 0;
                foreach (string statShortName in statShortNames) {
                    if (stat.Equals(statShortName, StringComparison.CurrentCultureIgnoreCase)) {
                        statValue = entity.GetStat(statIndex);
                        break;
                    }
                    statIndex ++;
                }
                formula = formula[0.. (index - 1)] + statValue + formula[(index + 4) ..];
                // Reset the index after a change.
                index = -1;
            }
        }
        return formula;
    }

    /// <summary>
    /// Private helper function that calculates the results of a clean string formula using BIDMAS.
    /// </summary>
    /// <param name="formula">Formula that has been cleaned without any stat short names.</param>
    /// <returns>The result of the calculation.</returns>
    private static string Calculate(string formula) {
        for (int index = 0; index < formula.Length; index ++) {
            char character = formula[index];
            // Resolve brackets first.
            if (character == '(') {
                // Find the related closing bracket.
                string formulaAfterBracket = formula[index..];
                // Check number of internal brackets within the bracket.
                int numberOfBrackets = 0;
                for (int bracketIndex = 1; bracketIndex < formulaAfterBracket.Length; bracketIndex ++) {
                    char bracketCharacter = formulaAfterBracket[bracketIndex];
                    // If another internal bracket exists we should skip a related closing bracket.
                    if (bracketCharacter == '(') {
                        numberOfBrackets ++;
                    }
                    // If an internal bracket closes we can decrement the counter otherwsie we calculate the brackets value.
                    else if (bracketCharacter == ')') {
                        if (numberOfBrackets > 0) {
                            numberOfBrackets --;
                        }
                        else {
                            // Update the formula based on these values.
                            // Recursion used here as formulas can contain many internal brackets etc.
                            if (index > 1) {
                                formula = formula[0.. index] + Calculate(formulaAfterBracket[1.. bracketIndex]) + formulaAfterBracket[(bracketIndex + 1)..];
                            }
                            else {
                                formula = Calculate(formulaAfterBracket[1.. bracketIndex]) + formulaAfterBracket[(bracketIndex + 1)..];
                            }
                            index = -1;
                            break;
                        }
                    }
                }
            }
        }

        for (int index = 0; index < formula.Length; index ++) {
            char character = formula[index];
            // Resolve indicies next.
            if (character == '^') {
                int[] boundNumbers = GetBoundNumbers(formula, index);
                int value = (int) Math.Pow(boundNumbers[2], boundNumbers[3]);
                formula = formula[0.. boundNumbers[0]] + value.ToString() + formula[boundNumbers[1]..];
            }
        }

        for (int index = 0; index < formula.Length; index ++) {
            char character = formula[index];
            // Resolve divison next.
            if (character == '/') {
                int[] boundNumbers = GetBoundNumbers(formula, index);
                int value = boundNumbers[2] / boundNumbers[3];
                formula = formula[0.. boundNumbers[0]] + value.ToString() + formula[boundNumbers[1]..];
            }
        }

        for (int index = 0; index < formula.Length; index ++) {
            char character = formula[index];
            // Resolve multiplication next.
            if (character == '*') {
                int[] boundNumbers = GetBoundNumbers(formula, index);
                int value = boundNumbers[2] * boundNumbers[3];
                formula = formula[0.. boundNumbers[0]] + value.ToString() + formula[boundNumbers[1]..];
            }
        }

        for (int index = 0; index < formula.Length; index ++) {
            char character = formula[index];
            // Resolve addition next.
            if (character == '+') {
                int[] boundNumbers = GetBoundNumbers(formula, index);
                int value = boundNumbers[2] + boundNumbers[3];
                formula = formula[0.. boundNumbers[0]] + value.ToString() + formula[boundNumbers[1]..];
            }
        }

        for (int index = 0; index < formula.Length; index ++) {
            char character = formula[index];
            // Resolve subtraction last.
            if (character == '-') {
                int[] boundNumbers = GetBoundNumbers(formula, index);
                int value = boundNumbers[2] - boundNumbers[3];
                formula = formula[0.. boundNumbers[0]] + value.ToString() + formula[boundNumbers[1]..];
            }
        }

        return formula;
    }

    /// <summary>
    /// Returns the index before an operations index and after an operation index.
    /// </summary>
    /// <param name="formula">Formula that is checked using the mid index value.</param> 
    /// <param name="midIndex">The index of the operation.</param>
    /// <returns>2 integers, the first the left index of the left most point before the operation and the 2nd 
    /// the right most point of the operation.</returns>
    private static int[] GetBoundNumbers(string formula, int midIndex) {
        int leftIndex;
        int rightIndex;
        // First loop to find left index.
        for (leftIndex = midIndex - 1; leftIndex > 0; leftIndex --) {
            char character = formula[leftIndex];
            if (!char.IsNumber(character)) {
                leftIndex ++;
                break;
            }
        }
        // First loop to find left index.
        for (rightIndex = midIndex + 1; rightIndex < formula.Length; rightIndex ++) {
            char character = formula[rightIndex];
            if (!char.IsNumber(character)) {
                break;
            }
        }
        int leftNumber = int.Parse(formula[leftIndex.. midIndex]);
        int rightNumber = int.Parse(formula[(midIndex + 1).. rightIndex]);
        return [leftIndex, rightIndex, leftNumber, rightNumber];
    }

}