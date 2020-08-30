using System;
using System.Collections.Generic;
using System.Linq;

namespace Combinations
{
    public static class VariableCombinationGenerator
    {
        public static void GenerateAllVariableCombinationsRecursive(
            string[] variables,
            string[] cumulatedList,
            List<List<string>> cumulatedLists, 
            int desiredNbOfVariables)
        {
            if (cumulatedList.Length == desiredNbOfVariables)
            {
                cumulatedLists.Add(cumulatedList.ToList());
                return;
            }

            for (int i = 0; i < variables.Length; i++)
            {
                var newVariables = new ArraySegment<string>(variables, i + 1, variables.Length - i - 1)
                    .ToArray();

                var newCumulatedList = new string[cumulatedList.Length + 1];
                Array.Copy(cumulatedList, 0, newCumulatedList, 0, cumulatedList.Length);
                newCumulatedList[newCumulatedList.Length - 1] = variables[i];
    
                GenerateAllVariableCombinationsRecursive(
                    newVariables,
                    newCumulatedList,
                    cumulatedLists,
                    desiredNbOfVariables);
            }
        }

        public static List<List<string>> GenerateAllVariableCombinations(List<Variable> variables)
        {
            var combinations = new List<List<string>>();

            for (int i = 1; i < variables.Count + 1; i++)
            {
                var result = new List<List<string>>();
                GenerateAllVariableCombinationsRecursive(
                    variables.Select(v => v.Name).ToArray(), 
                    new string[0], 
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
