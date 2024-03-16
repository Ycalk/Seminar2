using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Seminar2.Generator;

namespace Seminar2.Expression
{
    internal static class Converter
    {
        private static int GetPriority(this LayerElement element) =>
            element.Value switch
            {
                "+" => 1,
                "-" => 1,
                "*" => 2,
                _ => 0
            };

        public static string ConvertToInfixNotation(LayerElement[] elements)
        {
            var result = new StringBuilder();
            result.Append(elements[0].Value);
            var priority = 0;
            for (var i = 2; i < elements.Length; i += 2)
            {
                var element = elements[i].Value;
                var elementOperator = elements[i-1];
                if (priority == 0 || elementOperator.GetPriority() <= priority)
                    result.Append(elementOperator.Value + element);
                else
                {
                    result.Insert(0, "(");
                    result.Append(")" + elementOperator.Value + element);
                }
                priority = elementOperator.GetPriority();
            }

            return result.ToString();
        }
    }
}
