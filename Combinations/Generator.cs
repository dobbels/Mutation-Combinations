using System;
using System.Collections.Generic;
using System.Linq;

namespace Combinations
{
    public static class Generator
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

            // als er geen meer over zijn moet je returnen zonder iets toe te voegen aan cumulatedLists
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

        public static List<List<Mutation>> GenerateMutations(this (List<Variable> variables, I28Variable i28Variable) inputTuple, int numberOfMutations)
        {
            return inputTuple.variables.Select(v => v.GenerateMutations(inputTuple.i28Variable)).ToList();
        }

        public static List<Mutation> GenerateMutations(this Variable variable, I28Variable i28Variable)
        {
            var mutations = new List<Mutation>();

            foreach (var variableCondition in variable.Conditions)
            {
                foreach (var i28VariableCondition in i28Variable.Conditions)
                {
                    var mutation = new Mutation();
                    mutation.Sequence.Add(variableCondition);
                    mutation.Sequence.Add(i28VariableCondition);
                    mutations.Add(mutation);
                }
            }

            return mutations;
        }
    }
}
