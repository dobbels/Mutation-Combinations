﻿using System.Collections.Generic;
using System.Linq;

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