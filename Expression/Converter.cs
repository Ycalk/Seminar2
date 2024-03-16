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
            element.Element switch
            {
                "+" => 1,
                "-" => 1,
                "*" => 2,
                _ => 0
            };

        public static string ConvertToInfixNotation(LayerElement[] elements)
        {
            var result = new StringBuilder();
            result.Append(elements[0].Element);
            var priority = 0;
            for (var i = 2; i < elements.Length; i += 2)
            {
                var element = elements[i].Element;
                var sing = elements[i-1];
                if (priority == 0 || sing.GetPriority() <= priority)
                {
                    result.Append(sing.Element + element);
                    priority = sing.GetPriority();
                }
                else
                {
                    result.Insert(0, "(");
                    result.Append(")");
                    result.Append(sing.Element + element);
                    priority = sing.GetPriority();
                }
            }

            return result.ToString();
        }
    }
}
