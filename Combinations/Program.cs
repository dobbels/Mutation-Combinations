using System;
using System.Linq;

namespace Combinations
{
    public class Program
    {
        static void Main(string[] args)
        {
            var (variables, i28Variable) = ExcelReader.GetVariableConditions("Variables.xlsx");

            foreach (var variable in variables)
            {
                Console.WriteLine(variable);
            }

            Console.WriteLine(i28Variable);

            // generate variable combinations for number of mutations and bundle them all
            var variableCombinations = Generator.GenerateAllVariableCombinations(variables);
            variableCombinations.ForEach(vc => Console.WriteLine(vc.Aggregate((current, next) => current + "," + next)));
            
            for (int nbOfMutations = 1; nbOfMutations <= 8; nbOfMutations++)
            {
                
                // generate all possible combinations by 'filling in' the conditions in the variables

                // always add possibilities for I28 after

                var mutationSequences = (variables, i28Variable).GenerateMutations(nbOfMutations);

                foreach (var mutationList in mutationSequences)
                {
                    mutationList.ForEach(Console.WriteLine); // muy elegante
                    Console.WriteLine();
                }
            }
        }
    }
}
