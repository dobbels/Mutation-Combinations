using CombinationsGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationsGenerator.Generators
{
    public static class MutationGenerator
    {
        public static List<Mutation> GenerateMutationsOf(List<List<string>> variableCombinations, List<Variable> variables)
        {
            var mutations = new List<Mutation>();
            foreach (var variableCombination in variableCombinations)
            {
                var mutationsForThisCombination = new List<Mutation>();

                // TODO op de een of andere manier is het hier normaal mogelijk om ToDictionary te gebruiken
                var variableDictionary = new Dictionary<string, List<string>>();
                foreach (var variable in variables)
                {
                    variableDictionary.Add(variable.Name, variable.Conditions.Select(c => c.Name).ToList());
                }

                GenerateMutationsOf(variableCombination, variableDictionary, mutationsForThisCombination);

                mutations.AddRange(mutationsForThisCombination);
            }

            return mutations;
        }

        private static void GenerateMutationsOf(List<string> variableCombination, Dictionary<string, List<string>> variableDictionary, List<Mutation> mutationsForThisCombination)
        {
            var listsOfVariableConditions = new List<List<string>>();
            foreach (var variable in variableCombination)
            {
                var result = variableDictionary.TryGetValue(variable, out var conditions);
                if (!result)
                    throw new KeyNotFoundException(variable);
                listsOfVariableConditions.Add(conditions);
            }

            if (listsOfVariableConditions.Count != variableCombination.Count)
                throw new Exception("listsOfVariableConditions wrongly calculated");

            GenerateMutationsRecursively(listsOfVariableConditions, new List<string>(), mutationsForThisCombination);
        }

        private static void GenerateMutationsRecursively(
            List<List<string>> listsOfVariableConditions,
            List<string> cumulatedConditions,
            List<Mutation> mutationsForThisCombination)
        {
            if (listsOfVariableConditions.Count == 0)
            {
                mutationsForThisCombination.Add(cumulatedConditions.ToMutation());
                return;
            }

            for (var i = 0; i < listsOfVariableConditions[0].Count; i++)
            {
                cumulatedConditions.Add(listsOfVariableConditions[0][i]);

                GenerateMutationsRecursively(
                    listsOfVariableConditions.Skip(1).ToList(),
                    cumulatedConditions,
                    mutationsForThisCombination);

                cumulatedConditions.Remove(listsOfVariableConditions[0][i]);
            }
        }
    }
}
