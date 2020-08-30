using System;

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

            Console.WriteLine("How many variables do you want to include");
            var nbOfVariables = Console.ReadLine();
            while (!string.IsNullOrEmpty(nbOfVariables) && !nbOfVariables.Equals("q"))
            {
                var variableIndex = 1;
                try
                {
                    var inputNumber = Convert.ToInt32(nbOfVariables);
                    var mutations = (variables, i28Variable).GenerateMutations(inputNumber, variableIndex);

                    foreach (var mutationList in mutations)
                    {
                        mutationList.ForEach(Console.WriteLine); // muy elegante
                        Console.WriteLine();
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                nbOfVariables = Console.ReadLine();
            }
        }
    }
}
