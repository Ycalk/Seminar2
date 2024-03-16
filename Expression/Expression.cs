using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Seminar2.Generator;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Seminar2.Expression
{
    internal class Expression
    {
        // E = a1 * x^(l+1) + a2 * x^l + ...
        // index - power
        // element - coefficient
        private readonly int[] _expressionElements;

        private readonly int _hash = 1009;
        private readonly string _infixNotation;

        public ReadOnlyCollection<int> Elements => Array.AsReadOnly(_expressionElements);

        private void ParseNumber(LayerElement element, LayerElement sing)
        {
            if (element.ElementType != LayerElement.Type.Number)
                throw new ArgumentException("Invalid input element type");
            var convertedElement = int.Parse(element.Element);
            switch (sing.Element)
            {
                case "+":
                    _expressionElements[0] += convertedElement;
                    break;
                case "-":
                    _expressionElements[0] -= convertedElement;
                    break;
                case "*":
                    for (var i = 0; i < _expressionElements.Length; i++)
                        _expressionElements[i] *= convertedElement;
                    break;
            }
        }

        private void ParseVariable(LayerElement sing)
        {
            switch (sing.Element)
            {
                case "+":
                    _expressionElements[1] += 1;
                    break;
                case "-":
                    _expressionElements[1] -= 1;
                    break;
                case "*":
                    for (var i = _expressionElements.Length - 1; i > 0; i--)
                        _expressionElements[i] = _expressionElements[i - 1];
                    _expressionElements[0] = 0;
                    break;
            }
        }

        private void Parse(LayerElement element, LayerElement sing)
        {
            if (sing.ElementType != LayerElement.Type.Operator)
                throw new ArgumentException("Invalid input elements type");
            switch (element.ElementType)
            {
                case LayerElement.Type.Number:
                    ParseNumber(element, sing);
                    break;
                case LayerElement.Type.Variable:
                    ParseVariable(sing);
                    break;
                case LayerElement.Type.Operator:
                default:
                    throw new ArgumentException("Invalid element type");
            }
        }

        private void InitializeExpression(IReadOnlyList<LayerElement> elements)
        {
            Parse(elements[0], new LayerElement("+", LayerElement.Type.Operator));
            
            for (var i = 2; i < elements.Count; i+=2)
                Parse(elements[i], elements[i - 1]);
        }

        

        public Expression(LayerElement[] elements)
        {
            _expressionElements = new int[elements.Length / 2 + 2];
            InitializeExpression(elements);

            // hash
            foreach (var i in _expressionElements)
                _hash = (_hash * 9176) + i;

            _infixNotation = Converter.ConvertToInfixNotation(elements);
        }

        public override string ToString() => _infixNotation;

        public override int GetHashCode() => _hash;

        public override bool Equals(object? obj) => 
            _expressionElements.SequenceEqual(((obj as Expression)!).Elements);
    }
}
