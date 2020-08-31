using CombinationsGenerator.Models;
using System.Collections.Generic;
using System.Linq;

namespace CombinationsGenerator.Generators
{
    public static class VariableCombinationGenerator
    {
        public static void GenerateAllVariableCombinationsRecursive(
            List<string> variables,
            List<string> cumulatedList,
            List<List<string>> cumulatedLists,
            int desiredNbOfVariables)
        {
            if (cumulatedList.Count == desiredNbOfVariables)
            {
                cumulatedLists.Add(cumulatedList.ToList());
                return;
            }

            for (var i = 0; i < variables.Count; i++)
            {
                cumulatedList.Add(variables[i]);

                GenerateAllVariableCombinationsRecursive(
                    variables.Skip(i + 1).ToList(),
                    cumulatedList,
                    cumulatedLists,
                    desiredNbOfVariables);

                cumulatedList.Remove(variables[i]);
            }
        }

        public static List<List<string>> GenerateAllVariableCombinations(List<Variable> variables)
        {
            var combinations = new List<List<string>>();

            for (var i = 1; i < variables.Count + 1; i++)
            {
                var result = new List<List<string>>();
                GenerateAllVariableCombinationsRecursive(
                    variables.Select(v => v.Name).ToList(),
                    new List<string>(),
                    result, i);
                result.ForEach(lv => combinations.Add(lv));
#if DEBUG
                result.ForEach(lv => Console.WriteLine(String.Join(",", lv)));
#endif
            }

            return combinations;
        }
    }
}
