using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combinations
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
            return Sequence.Select(c => c.Name).Aggregate((current, next) => current + "," + next);
        }
    }
}
