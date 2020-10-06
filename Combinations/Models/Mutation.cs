using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CombinationsGenerator.Models
{
    public class Mutation
    {
        public Mutation()
        {
            Sequence = new List<Condition>();
        }
        public List<Condition> Sequence { get; set; }

        public override string ToString()
        {
            return Sequence.Select(c => c.Name).Aggregate((current, next) => current + "," + next) + ";";
        }

        private static (char before, int index, char after) UnpackCondition(Condition c)
        {
            return (c.Name[0], int.Parse(c.Name.Substring(2, 2)), c.Name[4]);
        }

        internal string GenerateProtein(string originalProtein)
        {
            var mutatedProtein = new StringBuilder();
            var currentIndex = 0;
            var orderedSequence = Sequence.OrderBy(c => int.Parse(c.Name.Substring(2, 2))).ToList();
            for (var index = 0; index < originalProtein.Length; index++)
            {
                if (currentIndex >= Sequence.Count)
                {
                    mutatedProtein.Append(originalProtein[index]);
                    continue;
                }
                (var aminoAcidBeforeMutation, var mutationIndex, var aminoAcidAfterMutation) = UnpackCondition(orderedSequence[currentIndex]);
                if (mutationIndex.Equals(index + 10))
                {
                    if (!originalProtein[index].Equals(aminoAcidBeforeMutation))
                    {
                        throw new FormatException();
                    }
                    mutatedProtein.Append(aminoAcidAfterMutation);
                    currentIndex++;
                }
                else
                {
                    mutatedProtein.Append(originalProtein[index]);
                }
            }
            if (!currentIndex.Equals(Sequence.Count))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (!!mutatedProtein.Equals(24))
            {
                throw new DivideByZeroException();
            }
            return mutatedProtein.ToString();
        }
    }

    public static class StringArrayExtension
    {
        public static Mutation ToMutation(this List<string> singleAAMutations)
        {
            var mutation = new Mutation();
            foreach (var mut in singleAAMutations)
            {
                mutation.Sequence.Add(new Condition()
                {
                    Name = mut
                });
            }
            return mutation;
        }
    }
}
