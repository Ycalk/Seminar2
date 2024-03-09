using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminar2.Generator;

namespace Seminar2.Expression
{
    internal class Expression
    {
        private readonly LayerElement[] _elements;

        public Expression(LayerElement[] elements)
        {
            _elements = new LayerElement[elements.Length];
            Array.Copy(elements, _elements, elements.Length);
        }
        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var layerElement in _elements)
                result.Append(layerElement.Element);
            return result.ToString();
        }
    }
}
