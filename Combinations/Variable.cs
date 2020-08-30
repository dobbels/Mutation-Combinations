using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Combinations
{
    public class Variable
    {
        public Variable(string name)
        {
            Name = name;
            Conditions = new List<Condition>();
        }

        public string Name { get; set; }

        public List<Condition> Conditions { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"Variable: {Name}\n");
            stringBuilder.Append("with conditions \n");
            foreach (var condition in Conditions)
            {
                stringBuilder.Append($"\t{condition.Name}");
            }

            stringBuilder.Append("\n");
            
            return stringBuilder.ToString();
        }
    }
}
