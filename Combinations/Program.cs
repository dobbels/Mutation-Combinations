using CombinationsGenerator.Generators;
using CombinationsGenerator.IO;
using System;
using System.IO;
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

            // generate variable combinations for each number of mutations and bundle them all
            var variableCombinations = VariableCombinationGenerator.GenerateAllVariableCombinations(variables);

            // generate all possible combinations by 'filling in' the conditions in the variables + add i28 variable conditions too
            foreach (var variableCombination in variableCombinations) //TODO kan dit niet simpeler? Met ForEach lijkt het alleszins niet te lukken
            {
                variableCombination.Add(i28Variable.Name);
            }
            variables.Add(i28Variable);
            var mutations = MutationGenerator.GenerateMutationsOf(variableCombinations, variables);

#if DEBUG
            for (var i = 1; i <= variables.Count; i++)
            {
                mutations.Where(m => m.Sequence.Count == i).ToList().ForEach(Console.WriteLine);
                Console.ReadKey();
            }
#endif

            // Output to text file
            var currentDirectory = Directory.GetCurrentDirectory();
            var fileName = $"all-combinations.txt";
            var filePath = Path.Combine(currentDirectory, fileName);
            using var file = new StreamWriter(fileName);
            foreach (var mutation in mutations)
            {
                file.WriteLine(mutation);
            }

            Console.WriteLine($"All mutations have been written to the file with location {filePath}.");
            Console.WriteLine();
            Console.WriteLine("Press any key to close this window");
            Console.ReadKey();
        }
    }
}