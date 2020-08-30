using System.Collections.Generic;
using System.Linq;

namespace Combinations
{
    public static class Generator
    {
        // Only Comb 1 x N for now
        public static List<List<Mutation>> GenerateMutations(this (List<Variable> variables, I28Variable i28Variable) inputTuple, int numberOfConditions, int variableIndex)
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
